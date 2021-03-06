using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10,100)] //esto es para que en la ui se vea como un rango para escoger entre el 10 y el 100
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function;
    

    Transform[] points;

    private void Awake()
    {
        points = new Transform[resolution * resolution];  //antes era solo resolution pero al incluir la z multiplicamos al cuadrado el numero de puntos que habran para que sea una matriz y no una linea
        float step = 2f / resolution;
        var scale = Vector3.one * step; //esto hace q todos los cubos tengan la misma escala y en funcion si son mas o menos tengan una menor o mayor escala
                                        //y que la escala por tanto no pase del rango -1 a 1, que en reaildad ahroa seria de 0 a 2, pero eso lo cambiaremos en la funcion de abajo

        for (int i = 0; i < points.Length; i++)
        {

            Transform point = points[i] = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }
    private void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        float step = 2f / resolution;
        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = f(u, v, time);
        }

    }

}


//posicion:   ((i + 0.5f) / 5f - 1f)
//____________
/*
 * Dice que cuando se trabaja con funciones, no es muy bueno utilizar posiciones rollo de 0 a todo lo largo que sea el mapa
 * o lo que sea, que lo mejor es que las posiciones sean de rango de 0 a 1 o de 1 a 1.
 *
 *Si hacemos que la instanciacion de los cubos sea x+1 pues si hacemos 10 estariamos cubriendo el rango de 0 a 9.
 *
 *Para hacer que sea de -1 a 1. Lo que podemos ahcer es, como tenemos 10 cubos, pues lo dividimos por 2 para tener rango de 0 a 2 y despues le restamos uno
 *de manera que todo quede entre -1 y 1. de manera que queda (i/5 - 1); //El 5 lo cambiaremos por otra variable mas adelante para que pueda adaptarse segun el numero de cubos q printemos
 *
 *Sin embargo, ahora no estaria realmente cubriendo de -1 a 1, porque tenemos 10 elementos y entonces seria:
 *-1, -.8, -.6, -.4, -.2, 0. .2, .4 ,.6 ,.8
 *
 *Y, adem?s, como los cubos miden 0.2 de ancho y el centro esta en su punto central, realmente el que empieza en -1
 *Llega a -1,1. Y el ultimo llega a 0,9
 *
 *Para intentar cubrir el rango m?ximo que podemos en este caso, desplazamos todo 0,5 para que el rango sea de 0.9 a - 0.9. Asi q  sumamos el 0.5f en la i antes de ser dividida
 */ 

//escala:
//__________
/*Ahoar tenemos que ahcer lo mismo que con la posicion, pero con la escala.
 * Necesitamos que vaya del rango de -1 a 1.
 * Asiq ue lo que podemos hacer es, hacer que la resolucion tenga el valor 2 dividido entre el numero de 
 * cubos que vayamos a tener. As? al calcular la escala, como multiplicamos el vector por la resolucion,
 * que es en funcion del numero de cubos que hay, que previamente se calcula para que vaya del rango de 0 a 2.
 * pos aqui obtendremos la escala en base a ese rango, ya que en funcion de si sean mas o menos se modificar?
 * 
 * 
 * 
 * 
 */