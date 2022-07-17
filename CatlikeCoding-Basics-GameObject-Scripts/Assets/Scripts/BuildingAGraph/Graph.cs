using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10,100)] //esto es para que en la ui se vea como un rango para escoger entre el 10 y el 100
    int resolution = 10;

    Transform[] points;

    private void Awake()
    {
        points = new Transform[resolution];
        float step = 2f / resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step; //esto hace q todos los cubos tengan la misma escala y en funcion si son mas o menos tengan una menor o mayor escala
        //y que la escala por tanto no pase del rango -1 a 1, que en reaildad ahroa seria de 0 a 2, pero eso lo cambiaremos en la funcion de abajo
        
        for (int i = 0; i < points.Length; i++)
        {
          
            Transform point = Instantiate(pointPrefab); //cuando instanciamos un prefab lo que hace es añadirlo a la escena
                                                        //En este caso instanciamos el prefab pero dentro del objeto point que tenemos que es de tipo transformpoints[                
            
            points[i] = point;
            
            point.SetParent(transform, false); //esto es para que en la jerarquia no aparezcan las isntancias como objetos ahi sueltos, sino que esten dentro del empyobject del graph
                                                //Y el false es para que el componente transform de las isntancias no sea el mismo que el del padre, que es lo que haria si pusiera true
            position.x = (i + 0.5f) * step - 1f;
           
            point.localPosition = position;//vector3.right es lo mismo que Vector3(1,0,0).
            point.localScale = scale;
            
            
        }
    }
    private void Update()
    {
        float time = Time.time;
        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time)); //position.y = Mathf.Sin(position.x); como nuestra posicion solo va de -1 a 1 y el sin se da en 2pi, podemos multiplicarlo por pi para que se de toda la funcion y no solo la parte que iria de -1 a 1
            point.localPosition = position;

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
 *Y, además, como los cubos miden 0.2 de ancho y el centro esta en su punto central, realmente el que empieza en -1
 *Llega a -1,1. Y el ultimo llega a 0,9
 *
 *Para intentar cubrir el rango máximo que podemos en este caso, desplazamos todo 0,5 para que el rango sea de 0.9 a - 0.9. Asi q  sumamos el 0.5f en la i antes de ser dividida
 */ 

//escala:
//__________
/*Ahoar tenemos que ahcer lo mismo que con la posicion, pero con la escala.
 * Necesitamos que vaya del rango de -1 a 1.
 * Asiq ue lo que podemos hacer es, hacer que la resolucion tenga el valor 2 dividido entre el numero de 
 * cubos que vayamos a tener. Así al calcular la escala, como multiplicamos el vector por la resolucion,
 * que es en funcion del numero de cubos que hay, que previamente se calcula para que vaya del rango de 0 a 2.
 * pos aqui obtendremos la escala en base a ese rango, ya que en funcion de si sean mas o menos se modificará
 * 
 * 
 * 
 * 
 */