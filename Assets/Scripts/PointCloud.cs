using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class PointCloud : MonoBehaviour
{
    public TextAsset binData;
    public MeshFilter starSet;

    const int MAX_POINTS = 60000;
    
    struct Pt
    {
        public float mag, ra, dec, parallax;
    }


    void Start()
    {
        MemoryStream stream = new MemoryStream(binData.bytes);
        BinaryReader br = new BinaryReader(stream);
        Pt[] pts = new Pt[MAX_POINTS];
        int ptsCount = 0;

        try
        {
            while (true)
            {
                Pt pt = new Pt();
                pt.mag = br.ReadSingle();
                pt.ra = br.ReadSingle();
                pt.dec = br.ReadSingle();
                pt.parallax = br.ReadSingle();
                pts[ptsCount++] = pt;
                if (ptsCount == MAX_POINTS)
                {
                    CreateMesh(pts, ptsCount);
                    ptsCount = 0;
                }
            }
        }
        catch (EndOfStreamException)
        {
        }
        if (ptsCount > 0)
            CreateMesh(pts, ptsCount);
    }

    void CreateMesh(Pt[] pts, int numPoints)
    {
        Vector3[] points = new Vector3[numPoints];
        int[] indices = new int[numPoints];
        Color[] colors = new Color[numPoints];

        for (int i = 0; i < points.Length; ++i)
        {
            Pt pt = pts[i];
            float cosdec = Mathf.Cos(Mathf.Deg2Rad * pt.dec);

            points[i] = new Vector3(
                -Mathf.Sin(Mathf.Deg2Rad * pt.ra) * cosdec,
                Mathf.Sin(Mathf.Deg2Rad * pt.dec),
                Mathf.Cos(Mathf.Deg2Rad * pt.ra) * cosdec) / pt.parallax;

            indices[i] = i;

            /* Turn the magnitude into brightness.
             */
            float col = Mathf.Pow(2.512f, 6.0f - pt.mag);
            colors[i] = new Color(col, col, col, 1.0f);
        }

        Mesh mesh = new Mesh();
        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indices, MeshTopology.Points, 0);

        MeshFilter mf = Instantiate<MeshFilter>(starSet, transform);
        mf.mesh = mesh;
        mf.gameObject.SetActive(true);
    }
}