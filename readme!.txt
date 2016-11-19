SpaceCraftInvaders v bug.bug.bug (?)
t6 v alfa0.0.5

comandos: (deberían funcionar, quien sabe =D)
flechas izquierda / derehcha: mover cañón
espacio disparar cañón (arma actual)
ctrl (derecho/izquierdo) cambiar de arma... orden: láser, rocket, plasma, nuke... si una no tiene balas, obviamente no es seleccionable =/.
1. (teclado normal): comprar 10 lásers
2. comprar 5 rockets
3. comprar 3 plasmas
4. comprar nuke
s. crear SCV
0: activar sonidos (En la verción actual funcionan sin problemas.)
9: activar/desactibar música... (Funciona perfecto)
suprimir: cheat: suicidarse xD... autoperder.
Flecha Hacia arriba: Crear escudo.
r. Implementar mejora mirror(hace que los escudos al ser impactados, disparen automáticamente una bala con el mismo daño resivido... inclúyase el reflejo de nukes para el modo de p2p)
a. Implementar mejora estándar de escudo. Le sube un nivel a los escudos, aumentando su hp. 
Hp normal: 1. Luego de la primera mejora 2, y finalmente 3.
u. Implementar mejora de SCV: proboca que se muevan más rápido, recolecten mayor cantidad de mineral, y tarden menos en recogerlo de la mina.
h: Curar hp del cañón:
m: recargar mineral de la mina (sólo funciona si es que esta aún existe)
shift: cheat: eliminar el primer miembro de el grupo actual, léase SCV o escudo.
Para el jugador dos los comandos son todos iguales.

En la verción actual, el modo 1 jugador (que posee estilo faster y el normal) se puede jugar bien, tiene nomás el problema que no se reparó de que las naves enemigas se estaquean, y la parte gráfica es bastante cuadrada. Se maneja sólo con labels y cuadritos, por lo que cuando en la pantalla ya hay muchas cosas, cuesta identificarlo todo.

Para el modo jugador, los constructores, naves, tiempos, y provavilidades sí están correctos, junto con que cada una de las naves se diferencia de las otras.

Al menos, aparentemente los constructores sí funcionan bien, el sistema de compra de recursos es perfecto, la nuke sí limpia todo xD, y si una bala hace contacto, el puntaje sí aumenta... el temporizador sí fluye, los niveles transcurren, las naves sí cumplen con las características de provavilidad de entrada / disparo.
También cambian el hp de las naves y otros datos.
A, en el modo faster, las naves enemigas son más duras, aparesen más rápido, hacen más daño y disparan con mayor frecuencia.

alerta! no mover ningún archivo de sonido, sino ... quien sabe que pase =/ 

update:
añadida la función de heal:
comando tecla h, tiene costo en minerales.

verción actual:

Se incluyó una pequeña pantalla de inicio, que es la que permite escoger entre los modos disponibles. Single Player, o MultiPlayer.
En multiplayer, se escoge ser Host, o Client...

En la pantalla de host, se escogen:
1. Vida de los cañones
2. Máximo mineral de las minas
3. Mineral con el que inician los jugadores.
4. puerto de conección
Al lado de eso, sale otro cuadro de texto, en la que se enlistan las IPS actuales del computador en el que se ejecuta el programa... hay un error gráfico, que este último cuadro tapa a los demás...., lo otro, que en el cuadro enlista sin querer también las IPS v6.

En la pantalla de Client, sólo hay dos recuadros... el de IP, en el cual hay que colocar la IP con la que se desea conectar, y el puerto a utilizar.
Al precionar el botón create game de la pantalla en host, el juego se quedará esperando una conección... si se quiere cerrar, hay que abortar proceso, porque se bloquea si no llega nada.
En el caso de Join Game en client, buscará una partida... si no encuentra nada, arrojará un error con un cuadro de diálogo, y al dar aceptar, cerrará el juego... el tiempo en el que aparese dicho recuadro, tarda un poco, porque intenta varias veces estableser conección.

En la verción actual, la pantalla del jugador 1 es perfecta; se ve todo bien, y lo que ejecute el jugador 2 se manifiesta sin errores, corriendo el juego como debería ser.
En la pantalla del jugador 2, no obstante, hay errores de sincronisación gráfica; las balas no se mueven, y ciertos objetos al ser destruidos, no desaparesen y se quedan estático ahí...
Los sonidos si funcionan bien, y el envío de comandos, como la notificación del fin del juego, son correctas.

Un error a reparar, es que al cerrar el juego, por conveniencia, el jugador 2 ha de salir primero, porque se produce un error entre los canales que al acabar el juego no se cierran.

El problema eso sí, es que tiene un tanto bastante lag, en especial cuando la acumulación de objetos en la pantalla es mucha... la idea utilizada es pésima, pero por tiempo, no alcansaba a rediseñarlo.

En fin, espero que haya lo suficiente como para el 4 con esto...


update: reparadas algunas cosas, funcioona bien ahora, reparada la notificación final de ganador/perdedor, y quitadas cosas de los logs para hacerlos más livianos...

Como detalle, al ejecutar la tarea por vs, se laguea conciderablemente mucho más en comparación si se ejecuta directamente el archivo ejecutable.

A, como otro detalle, para ejecutar dos instancias del programa, se recomiendo copiar todo el output (la carpeta bin/debug) a otra ruta, y cambiarle el nombre al exe... no sé por qué, pero el sistema no deja ejecutar dos veces el mismo ejecutable en la misma dirección... me imagino que es por la utilización de los dlls externos de la librería irckLang

