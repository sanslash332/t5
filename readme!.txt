SpaceCraftInvaders v bug.bug.bug (?)
t6 v alfa0.0.5

comandos: (deber�an funcionar, quien sabe =D)
flechas izquierda / derehcha: mover ca��n
espacio disparar ca��n (arma actual)
ctrl (derecho/izquierdo) cambiar de arma... orden: l�ser, rocket, plasma, nuke... si una no tiene balas, obviamente no es seleccionable =/.
1. (teclado normal): comprar 10 l�sers
2. comprar 5 rockets
3. comprar 3 plasmas
4. comprar nuke
s. crear SCV
0: activar sonidos (En la verci�n actual funcionan sin problemas.)
9: activar/desactibar m�sica... (Funciona perfecto)
suprimir: cheat: suicidarse xD... autoperder.
Flecha Hacia arriba: Crear escudo.
r. Implementar mejora mirror(hace que los escudos al ser impactados, disparen autom�ticamente una bala con el mismo da�o resivido... incl�yase el reflejo de nukes para el modo de p2p)
a. Implementar mejora est�ndar de escudo. Le sube un nivel a los escudos, aumentando su hp. 
Hp normal: 1. Luego de la primera mejora 2, y finalmente 3.
u. Implementar mejora de SCV: proboca que se muevan m�s r�pido, recolecten mayor cantidad de mineral, y tarden menos en recogerlo de la mina.
h: Curar hp del ca��n:
m: recargar mineral de la mina (s�lo funciona si es que esta a�n existe)
shift: cheat: eliminar el primer miembro de el grupo actual, l�ase SCV o escudo.
Para el jugador dos los comandos son todos iguales.

En la verci�n actual, el modo 1 jugador (que posee estilo faster y el normal) se puede jugar bien, tiene nom�s el problema que no se repar� de que las naves enemigas se estaquean, y la parte gr�fica es bastante cuadrada. Se maneja s�lo con labels y cuadritos, por lo que cuando en la pantalla ya hay muchas cosas, cuesta identificarlo todo.

Para el modo jugador, los constructores, naves, tiempos, y provavilidades s� est�n correctos, junto con que cada una de las naves se diferencia de las otras.

Al menos, aparentemente los constructores s� funcionan bien, el sistema de compra de recursos es perfecto, la nuke s� limpia todo xD, y si una bala hace contacto, el puntaje s� aumenta... el temporizador s� fluye, los niveles transcurren, las naves s� cumplen con las caracter�sticas de provavilidad de entrada / disparo.
Tambi�n cambian el hp de las naves y otros datos.
A, en el modo faster, las naves enemigas son m�s duras, aparesen m�s r�pido, hacen m�s da�o y disparan con mayor frecuencia.

alerta! no mover ning�n archivo de sonido, sino ... quien sabe que pase =/ 

update:
a�adida la funci�n de heal:
comando tecla h, tiene costo en minerales.

verci�n actual:

Se incluy� una peque�a pantalla de inicio, que es la que permite escoger entre los modos disponibles. Single Player, o MultiPlayer.
En multiplayer, se escoge ser Host, o Client...

En la pantalla de host, se escogen:
1. Vida de los ca�ones
2. M�ximo mineral de las minas
3. Mineral con el que inician los jugadores.
4. puerto de conecci�n
Al lado de eso, sale otro cuadro de texto, en la que se enlistan las IPS actuales del computador en el que se ejecuta el programa... hay un error gr�fico, que este �ltimo cuadro tapa a los dem�s...., lo otro, que en el cuadro enlista sin querer tambi�n las IPS v6.

En la pantalla de Client, s�lo hay dos recuadros... el de IP, en el cual hay que colocar la IP con la que se desea conectar, y el puerto a utilizar.
Al precionar el bot�n create game de la pantalla en host, el juego se quedar� esperando una conecci�n... si se quiere cerrar, hay que abortar proceso, porque se bloquea si no llega nada.
En el caso de Join Game en client, buscar� una partida... si no encuentra nada, arrojar� un error con un cuadro de di�logo, y al dar aceptar, cerrar� el juego... el tiempo en el que aparese dicho recuadro, tarda un poco, porque intenta varias veces estableser conecci�n.

En la verci�n actual, la pantalla del jugador 1 es perfecta; se ve todo bien, y lo que ejecute el jugador 2 se manifiesta sin errores, corriendo el juego como deber�a ser.
En la pantalla del jugador 2, no obstante, hay errores de sincronisaci�n gr�fica; las balas no se mueven, y ciertos objetos al ser destruidos, no desaparesen y se quedan est�tico ah�...
Los sonidos si funcionan bien, y el env�o de comandos, como la notificaci�n del fin del juego, son correctas.

Un error a reparar, es que al cerrar el juego, por conveniencia, el jugador 2 ha de salir primero, porque se produce un error entre los canales que al acabar el juego no se cierran.

El problema eso s�, es que tiene un tanto bastante lag, en especial cuando la acumulaci�n de objetos en la pantalla es mucha... la idea utilizada es p�sima, pero por tiempo, no alcansaba a redise�arlo.

En fin, espero que haya lo suficiente como para el 4 con esto...


update: reparadas algunas cosas, funcioona bien ahora, reparada la notificaci�n final de ganador/perdedor, y quitadas cosas de los logs para hacerlos m�s livianos...

Como detalle, al ejecutar la tarea por vs, se laguea conciderablemente mucho m�s en comparaci�n si se ejecuta directamente el archivo ejecutable.

A, como otro detalle, para ejecutar dos instancias del programa, se recomiendo copiar todo el output (la carpeta bin/debug) a otra ruta, y cambiarle el nombre al exe... no s� por qu�, pero el sistema no deja ejecutar dos veces el mismo ejecutable en la misma direcci�n... me imagino que es por la utilizaci�n de los dlls externos de la librer�a irckLang

