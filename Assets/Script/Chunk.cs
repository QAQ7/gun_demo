using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	private Mesh mesh;
	public int x, y, z;
	public GameObject createChunk;

	private List<Vector2> uv = new List<Vector2>();

	public Vector2[] uvs;

	//面需要的点
	private List<Vector3> vertices = new List<Vector3>();
	//生成三边面时用到的vertices的index
	private List<int> triangles = new List<int>();

	void Start()
	{
		//把所有面的点和面的索引添加进去
		makeChunk();
		//为点和index赋值

	}

	void Update(){
		
	}

	public void changeChunk(){
		createChunk.GetComponent<CreateChunk> ().id [x, y, z] = 0;
		createChunk.GetComponent<CreateChunk> ().blocksinorder [x , y, z] = null;
		if (x>0 && createChunk.GetComponent<CreateChunk> ().blocksinorder [x - 1, y, z] != null) {
			createChunk.GetComponent<CreateChunk> ().blocksinorder [x - 1, y, z].GetComponent<Chunk> ().AddFrontFace ();
			createChunk.GetComponent<CreateChunk> ().blocksinorder [x - 1, y, z].GetComponent<Chunk> ().complete ();
			Debug.Log ("Front");
		}
		if(y>0&&createChunk.GetComponent<CreateChunk>().blocksinorder[x,y-1,z]!=null){
			createChunk.GetComponent<CreateChunk>().blocksinorder[x,y-1,z].GetComponent<Chunk>().AddTopFace ();
			createChunk.GetComponent<CreateChunk> ().blocksinorder [x,y-1,z].GetComponent<Chunk> ().complete ();
			Debug.Log ("Top");
		}
		if(z>0&&createChunk.GetComponent<CreateChunk>().blocksinorder[x,y,z-1]!=null){
			createChunk.GetComponent<CreateChunk>().blocksinorder[x,y,z-1].GetComponent<Chunk>().AddRightFace ();
			createChunk.GetComponent<CreateChunk> ().blocksinorder [x,y,z-1].GetComponent<Chunk> ().complete ();
			Debug.Log ("Right");
		}
		if(x<4&&createChunk.GetComponent<CreateChunk>().blocksinorder[x+1,y,z]!=null){
			createChunk.GetComponent<CreateChunk>().blocksinorder[x+1,y,z].GetComponent<Chunk>().AddBackFace ();
			createChunk.GetComponent<CreateChunk> ().blocksinorder [x+1,y,z].GetComponent<Chunk> ().complete ();
			Debug.Log ("Back");
		}
		if(y<4&&createChunk.GetComponent<CreateChunk>().blocksinorder[x,y+1,z]!=null){
			createChunk.GetComponent<CreateChunk>().blocksinorder[x,y+1,z].GetComponent<Chunk>().AddBottomFace ();
			createChunk.GetComponent<CreateChunk> ().blocksinorder [x,y+1,z].GetComponent<Chunk> ().complete ();
			Debug.Log ("Bottom");
		}
		if(z<4&&createChunk.GetComponent<CreateChunk>().blocksinorder[x,y,z+1]!=null){
			createChunk.GetComponent<CreateChunk>().blocksinorder[x,y,z+1].GetComponent<Chunk>().AddLeftFace ();
			createChunk.GetComponent<CreateChunk> ().blocksinorder [x,y,z+1].GetComponent<Chunk> ().complete ();
			Debug.Log ("Left");
		}
	}

	public void makeChunk(){
		Debug.Log (x+" "+y+" "+z);
		if(x==4||createChunk.GetComponent<CreateChunk>().id[x+1,y,z]==0){
			AddFrontFace ();
			Debug.Log ("Front");
		}
		if(y==4||createChunk.GetComponent<CreateChunk>().id[x,y+1,z]==0){
			AddTopFace ();
			Debug.Log ("Top");
		}
		if(z==4||createChunk.GetComponent<CreateChunk>().id[x,y,z+1]==0){
			AddRightFace ();
			Debug.Log ("Right");
		}
		if(x==0||createChunk.GetComponent<CreateChunk>().id[x-1,y,z]==0){
			AddBackFace ();
			Debug.Log ("Back");
		}
		if(y==0||createChunk.GetComponent<CreateChunk>().id[x,y-1,z]==0){
			AddBottomFace ();
			Debug.Log ("Bottom");
		}
		if(z==0||createChunk.GetComponent<CreateChunk>().id[x,y,z-1]==0){
			AddLeftFace ();
			Debug.Log ("Left");
		}
		complete ();
	}

	public void getpos(){
		this.gameObject.name = this.gameObject.name.Replace ("Chunk(", "");
		this.gameObject.name = this.gameObject.name.Replace (")", "");
		string[] pos = this.gameObject.name.Split (',');
		x = int.Parse (pos [0]);
		y = int.Parse (pos [1]);
		z = int.Parse (pos [2]);
		Debug.Log (x+" "+y+" "+z);
	}

	public void complete(){
		mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		uvs = new Vector2[vertices.Count];
		for (int i = 0; i < vertices.Count; i += 4) {
			uvs[i] = new Vector2(0, 0);
			uvs[i+1] = new Vector2(0, 1);
			uvs[i+2] = new Vector2(1, 1);
			uvs[i+3] = new Vector2(1, 0);
		}
		mesh.uv = uvs;
		mesh.triangles = triangles.ToArray();

		//重新计算顶点和法线
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();

		//将生成好的面赋值给组件
		this.GetComponent<MeshFilter>().mesh = mesh;
	}

	//前面
	public void AddFrontFace()
	{
		//第一个三角面
		triangles.Add(0 + vertices.Count);
		triangles.Add(3 + vertices.Count);
		triangles.Add(2 + vertices.Count);

		//第二个三角面
		triangles.Add(2 + vertices.Count);
		triangles.Add(1 + vertices.Count);
		triangles.Add(0 + vertices.Count);


		//添加4个点
		vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
		vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
		vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
		vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));

	}

	//背面
	public void AddBackFace()
	{
		//第一个三角面
		triangles.Add(0 + vertices.Count);
		triangles.Add(3 + vertices.Count);
		triangles.Add(2 + vertices.Count);

		//第二个三角面
		triangles.Add(2 + vertices.Count);
		triangles.Add(1 + vertices.Count);
		triangles.Add(0 + vertices.Count);


		//添加4个点
		vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
		vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
		vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));
		vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));

	}

	//右面
	public void AddRightFace()
	{
		//第一个三角面
		triangles.Add(0 + vertices.Count);
		triangles.Add(3 + vertices.Count);
		triangles.Add(2 + vertices.Count);

		//第二个三角面
		triangles.Add(2 + vertices.Count);
		triangles.Add(1 + vertices.Count);
		triangles.Add(0 + vertices.Count);


		//添加4个点
		vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
		vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
		vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
		vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));

	}

	//左面
	public void AddLeftFace()
	{
		//第一个三角面
		triangles.Add(0 + vertices.Count);
		triangles.Add(3 + vertices.Count);
		triangles.Add(2 + vertices.Count);

		//第二个三角面
		triangles.Add(2 + vertices.Count);
		triangles.Add(1 + vertices.Count);
		triangles.Add(0 + vertices.Count);


		//添加4个点
		vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
		vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
		vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));
		vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));


	}

	//上面
	public void AddTopFace()
	{
		//第一个三角面
		triangles.Add(1 + vertices.Count);
		triangles.Add(0 + vertices.Count);
		triangles.Add(3 + vertices.Count);

		//第二个三角面
		triangles.Add(3 + vertices.Count);
		triangles.Add(2 + vertices.Count);
		triangles.Add(1 + vertices.Count);


		//添加4个点
		vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));
		vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
		vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
		vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));


	}

	//下面
	public void AddBottomFace()
	{
		//第一个三角面
		triangles.Add(1 + vertices.Count);
		triangles.Add(0 + vertices.Count);
		triangles.Add(3 + vertices.Count);

		//第二个三角面
		triangles.Add(3 + vertices.Count);
		triangles.Add(2 + vertices.Count);
		triangles.Add(1 + vertices.Count);


		//添加4个点
		vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
		vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
		vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
		vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));


	}


}
