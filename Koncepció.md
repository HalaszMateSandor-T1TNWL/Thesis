Az évek alatt sok olyan játékkal játszottam, amelyek mélyen kidolgozott mozgásrendszerekre vannak építve, ilyen műfajok például a: Movement-Shooter, bizonyos Sport játékok többnyire azok, amik gördeszkákra, görkorcsolyákra és a hasonlókra épülnek. Említettem, hogy a témámat nem a Black Ops 6 "[Omni-Movement](/Omni-Movement)" rendszere inspirálta, viszont fontosnak tartottam itt is megemlíteni a Movement-Shooter műfajt, mivel az ebbe tartozó játékok szerettették meg igazán velem a szofisztikált mozgásrendszereket és belőlük tanultam sokat.
A projekt maga viszont egy játszható prototípus, amely egyben egy hordozható rendszer is, így a terv szerint nem lesz műfajhoz és programhoz kötve, hanem mint egy Lego darab, kivehető és átrakható lesz egy másik programba.
## Játékmenet

Játékról, mint úgy nem lehet beszélni, viszont hordozható rendszerről már inkább. A mozgásrendszert úgy terveztem meg, hogy akármilyen olyan környezetben használható legyen, amelyben a játékos karakteren, vagy esetleg NPC-n (nem-játékos karakteren) valamilyen gurulásra vagy csúszásra alkalmas eszköz van (pl.: gördeszka, görkorcsolya, síléc, jégkorcsolya). Ehhez a Tony Hawk's Proskater játéksorozatot és Bomb Rush Cyberfunk / Jet Set Radio játékmenetét és mozgásrendszerét vettem alapul. Azzal az eltéréssel, hogy a prototípusban, megnyerési pont nincs, mivel ez túlmutatott a prototípusnak megszabott feladaton.
## Használt technológiák

A használt technológiákat két részre osztanám fel: Szoftver és Hardver. Most egyenlőre, csak egy rövidebb felsorolást tennék a technológiákról és a későbbiekben, amikor a megfelelő részekhez érek, bővebben beszámolok róluk.

Szoftver szempontjából:
- [Blender-t](https://www.blender.org/download/) használtam a 3Ds modellek elkészítéséhez.
- [Krita-t](https://krita.org/en/) használtam a Modellek textúráinak megrajzolásához és [SLK_img2pixel-t](https://captain4lk.itch.io/slk-img2pixel) használtam a színek állításához.
- A hangok megvágásához Audacity-t és LMSS-t használtam.
- A diagramok megszerkesztéséhez Violet-et használtam.
- A verziókövetéshez Git-et használtam.

Hardver szempontjából a szoftvert két különböző rendszeren teszteltem és fejlesztettem:
- [Gép összeállítása] (Windows)
- LENOVO IdeaPad Slim 3 15AMN8:
	- Kubuntu 24.04
	- Processzor: 8 x AMD Ryzen 5 7520U with Radeon Graphics
	- Videókártya: AMD Radeon 610M
	- Memória: 16GB
- Digitális rajzoláshoz az XP-Pen Deco 02-őt használtam
## Fejlesztési környezet

A fejlesztési környezet választásakor több szempontot is figyelembe kellett vennem:
- __Elérhetőség:__ Mivel a fejlesztést két különböző rendszeren, két különböző operációs rendszer alatt végeztem fontos volt számomra, hogy a fejlesztési környezet elérhető legyen mind a két operációs rendszeren.
- __Programozási Nyelv Támogatása:__ A Godot-n belül van számos programozási nyelvhez támogatás, ezek a: 
	- GDScript a Godot saját script nyelve,
	- C# a Godot Mono támogatásával,
	- Valamint C és C++ számos kiegészítő támogatásával.
- __Kényelem:__ A fejlesztési környezet által nyújtott kiegészítési, valamint emlékeztető segédletek fontosak voltak, mivel a rendszer sok hasonló kódrészt tartalmaz (pl. Dictionaries, Leképzések, stb.).
## Játékmotor


## Vizuális világ