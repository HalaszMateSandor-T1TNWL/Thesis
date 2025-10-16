# A Feladat Bemutatása

A feladatkiírásban a problémát úgy fogalmaztam meg, hogy: "Karaktermozgatás állapotgépekkel a Godot Engine-ben", ez javarészt le is írja a problémát, viszont ez egy kicsit mélyebbre megy. Nem csak egy videójáték karaktert fogunk itt megmozgatni, nem csak animációkat fogunk csinálni, de tisztázni fogjuk a különbségeket egy állapotgép és egy viselkedési fa között, melyiket mikor alkalmazzuk és miért, valamint össze fogjuk hasonlítani a futási időt és komplexitást egy átlagos if-else és egy állapotgép megoldás között.
## Kód struktúra

Ugyan az állapotgépek segítenek a programkód elegánsabb megjelenítésében és a bővíthetőség megkönnyítésében, a problémát viszont nem fogja megoldani: téged.

Ugyan az állapotgépes megközelítés segít a programkód elegánsabb megjelenítésében a végtelen sok változó helyett csak egy enumeráció használatával, és a bővíthetőségben szintúgy az enumeráció jellege és az állapotok közötti átjárás implementálásának köszönhetően, erre majd később kitérek.

Mindegy, hogy mennyire próbálkozunk a programkódunk előbb vagy utóbb egy spagetti szörnnyé fog változni. Ahhoz, hogy ez a szörny ne nőjön tovább és ne zavarjon bele a program többi részébe rá fogjuk tenni ezt a spagettit egy tányérra és a következő részt egy másik tányéron fogjuk elkezdeni. 
Ezt a programtervezési módot "Command" avagy Parancs tervezési módnak hívják. Kiküszöböljük a túlzott generalizálás problémáját, megelőzzük az oly gyakran elkövetett hibát ahol mindent túlgeneralizálunk, mindent felkészítünk az öröklésre és végül vagy fölösbe megy munka vele, vagy ott ragadunk több tíz alosztállyal és egy sérülékeny programrésszel, ahol egyetlen dolog átírása katasztrofális hibákat fog feldobni. Ez nem azt jelenti, hogy mellőzzük és teljesen kizárjuk az öröklődést, csupán csak egy kicsit háttérbe szorítjuk és nem feltétlenül arra összpontosítunk. 

## Adatkezelés

A legfontosabb egy videójáték megtervezésénél az az adatbázis pontos kidolgozása. Milyen adat, honnan, hova fog utazni és mikor. Csak gondoljunk bele, hogy mennyi adatcsere meg végbe egy egyszerű "A megüti B-t" interakciónál: Először is le kell kérdezni mind a két pozícióját ütközésvizsgálathoz, le kell kérdezni B életerejét és A sebzését, le kell vonni B életerejét és le kell kérdezni megint, hogy megnézzük nem nulla-e, mert ha igen akkor meg kell szüntetni B-t. Nem kevés lekérdezés ez és nagyon könnyen összeakadhatnak az adatok. Fontos a globális változókat kerülni, mert ha egyszerre többen is el akarják érni (egyik olvasni másik írni például), akkor össze __fognak__ akadni az adatok. 

# Állapotgépek

# Műfaj

# Tervezési Döntések