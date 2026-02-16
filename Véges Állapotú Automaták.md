
Ahogyan az informatika fejlődött és egyre több matematikai koncepciót adaptált, hogy segítse a fejlesztést, úgy távolodott is tőle. Meglepő módon nem kell ismernie az embernek a pontos matematikai definícióit egyes koncepcióknak annak érdekében, hogy használja őket fejlesztés során. Ez leginkább itt fog majd érződni, az automaták terén.
## Matematikai szempont

A véges állapotú automata (Finite State Machine, avagy FSM) egy matematikai model, amely képes ábrázolni a dinamikus viselkedését egy rendszernek véges számú állapotot alkalmazva, valamint ezen állapotok közötti átmenéseket, és az átmenetekhez szükséges cselekvéseket. Bármely adott pillanatban egy rendszer egy bizonyos állapotot vesz fel és egy átmenet egy válasz egy bizonyos cselekvésre, vagy történésre.
A véges állapotú automaták kategorizálhatóak **determinisztikus** és **non-determinisztikus** automatákra a viselkedésük szempontjából:
- **A determinisztikus véges automaták (DFSM)** esetében minden állapotnak van egy egyedi átmenete minden lehetséges bemenetre (input). A következő állapotot csakis a jelenlegi állapot és a kapott input dönti el, amely egy kiszámítható és egyértelmű viselkedést eredményez.
- **A non-determinisztikus automaták (NDFSM)** esetében több átmenet létezhet egy inputra és a jelenlegi állapotra. A rendszer következő állapota nem egyedien lesz eldöntve, ami rugalmasságot eredményez, de egyben kiszámíthatatlanságot és komplexitást is bevezet.
A mi esetünkben csak determinisztikus automatákkal fogunk foglalkozni, de érdemes volt megemlíteni a non-determinisztikus automatákat, mivel matematikai és tervezésbeli jelentőséggel bírnak.
Egy determinisztikus véges automata hivatalos definíciója így szól: 
$$
M = (Q, \Sigma, \delta, q_{0},F)
$$
Ahol:
1. Q, egy véges halmaz, az állapotok halmaza
2. ∑ , egy véges halmaz, az ABC
3. δ , Q x ∑ → Q az állapotátmenet függvény
4. $q_{0}$ ∈ Q a kezdeti állapot
5. F ⊆ Q a végállapotok halmaza 
Egy determinisztikus automata felismer (elfogad) egy jelsorozatot, ha w = $x_{1},x_{2}\dots x_{n}$ ∑ -beli jelek sorozata. M felismeri w-t, ha létezik olyan $r_{0},r_{1},\dots,r_{n}$ Q-beli állapotok sorozata, hogy:
- $r_{0} = q_{0}$
- $\delta(r_{i},x_{i+1}=r_{i+1})$, ahol $i=1,\dots n-1$
- $r_{n}$ ∈ F
__Definíció:__ M DFSM felismeri az A nyelvet, ha
$$
A = \{ w\space|\space M \space felismeri \space w-t\}
$$
**Jelölés:** A = L(M), avagy "A, az M automata nyelve"

Nézzük meg ezeket a részeket egy kicsit részletesebben is.

![[ExampleStateMachine.png]]
								Figure11

Egy egyszerű példa egy ajtó nyitására, nyitva tartására és csukására.

**Állapotok:**
- Az állapotok segítségével tudjuk megmondani, hogy milyen feltételek és módok mellett operál a rendszerünk a jelenlegi pillanatban. Minden állapot egy egyedi viselkedést ír le, amit a rendszer produkál. Fontos, hogy az állapotok száma *egy véges halmaz.*
- $Q_{1},\dots,Q_{n}$ -nel jelöljük az állapotokat, amikor vizualizáljuk az automatánk
**Átmenetek:** 
- Az átmenetek írják le a rendszer válaszait egyes bemenetekre, vagy eseményekre, amelyek átlökik az automatát az egyik állapotból a másikba.
- Csak bizonyos ingerek (stimuli, a példában $S_{1},S_{2},S_{3}$) eredményezhetnek átmenetet a rendszeren belül, olyanok amelyek benne vannak az automata **nyelvében.**
- Az átmeneteket mindig irányított nyilakkal jelöljük az ábrákon.
**Ingerek:**
- Az ingerek olyan események, vagy bemenetek, amelyek átmenetet eredményeznek az automatán belül. Lehetnek pl. felhasználói parancsok, szenzor érzékelések, hálózati üzenetek, stb.
**Válaszok:**
- Egy automata válasza (vagy kimenete) mindig egyedi az állapotot nézve. Ezek lehetnek: egy üzenet kiírása/elküldése, egy egész algoritmus lefuttatása, vagy csak egy változó felülírása.
- A példában a válaszokat (responses) az $R_{1},R_{2},R_{3}$ ábrázolja, az  $S_{1},S_{2},S_{3}$ ingerekre.

## Programozói szempont

Programozás szempontjából az automaták egy nagyon hasznos része az informatikának, mivel végtelenül egyszerűvé teszi egyes rendszerek nyomonkövetését, megtervezését és automatizálását.
Egy rendszeren belül, vegyünk most egy játékot példának, akármilyen objektumnak lehet állapota és, ahogyan ezt már említettem, az állapottól függően fognak változni az objektumok viselkedése is, például:
- A pálya időjárása (eshet, süthet a nap, fújhat a szél),
- a karakter fizikai állapota (futhat, ugorhat, csúszhat, támadhat),
- a fegyver állapota, amivel támadni akarunk (készenlétben, töltődik újra, elhasználva)
Vegyük észre, hogy az állapotok kihathatnak egymásra is, példaképpen: A karakterrel éppen csúszás közbe vagyunk és a csúszda végén ott van egy ellenfél, akit meg akarunk támadni, ezért elővesszük a fegyverünk, de ki van fogyva és ez átrak minket az "újratöltés" animációba, ami miatt csak belecsapódunk az ellenfélbe, a támadás helyett. Azzal, hogy mindig tisztában vagyunk vele, hogy milyen állapotban vannak a világunk egyes részei, megkönnyebbítjük a magunk dolgát a hibakeresésben és a játékosok dolgát azzal, hogy nem zavarjuk őket össze.
Amikor egy rendszernél állapotgépeket használunk, akkor általában kéz-a-kézben jár a
"kompozíció", és az "öröklődés" használata mint Objektum Orientált Programozásban használt tervezési minta. Ahhoz, hogy jobban megértsük hogyan is nézne ez ki kódban kezdés képpen készítettem egy mintaprogramot, amely mintaprogram egy egyszerű jelzőlámpa működését mutatja be.

Elsőként is létrehoztam egy vázat az állapotok számára, minden állapot egy külön Node lesz a Godot-n belül. Ez egy teljesen absztrakt osztály, amit valójában nem fogunk futtatni mint script, ezt csak örökölni fogják az egyes állapotaink. 
```C#
using Godot;
using Godot.Collections;

public partial class State : Node
{
	public StateMachine fsm;
	
	public virtual void Enter() {}
	public virtual void Exit() {}
	
	public virtual void Update(double delta) {}
	public virtual void PhysicsUpdate(double delta) {}
	
	public virtual void HandleInput(InputEvent @event) {}
	
}
```
Lehet, hogy megfordul a gondolat: "De, akkor miért nem egy absztrakt osztály?" Azért mert, akkor nem tudnánk felvenni az állapotokat egy Dictionary-be, erre később vissza is térek. Egy állapotnak 5 metódusa van: Enter, Exit, Update, PhysicsUpdate, HandleInput. Ezek mind a megfelelő pillanatban lesznek meghívva, lehet, hogy nem mindegyik állapot enged majd inputot feldolgozni, vagy lehet, hogy nem mindegyikre fog kihatni a fizika, viszont ez a metódus-ötös az alapja a legtöbb állapotnak. Az "Enter" metódusban az állapot ellenőrizni fog egy pár feltételt és fel fogja építeni az állapotban végrehajtható cselekvésekhez szükséges környezetet. Az "Exit" egyértelműen ennek az ellenkezőjét fogja csinálni. Az "Update" és "PhysicsUpdate" metódusok mind az eltelt (delta) idő szerint fognak végrehajtani eseményeket, pl.: a levegőben töltött idő mérése, esési sebesség, féktáv, stb. Végül a HandleInput akármilyen bemenetre fog reagálni és azokat lekezelni. Ha például a karakter éppen a levegőben van és elkezd mozogni, akkor azt elkapja a HandleInput metódus és megoldja, hogy ne csak egyhelyben tudjon a karakter ugrani, de különböző irányokba is. A metódusokon kívül van még egy változónk, ami az "fsm".

```C#
using Godot;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	[Export] public NodePath initialState;
	
	private System.Collections.Generic.Dictionary<string, State> _states;
	private State _currentState;
	
	public override void _Ready()
	{
		_states = new Dictionary<string, State>();
		foreach(Node node in GetChildren())
		{
			if(node is State s)
			{
				_states[node.Name] = s;
				s.fsm = this;
				s._Ready();
				s.Exit(); //reset all states
			}
		}
		if(initialState != null)
		{
			_currentState = GetNode<State>(initialState);
			_currentState.Enter();
		}
		else
			GD.Print("Initial State not set");
	}
	
	public override void _Process(double delta)
	{
		_currentState.Update((float)delta);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		_currentState.PhysicsUpdate(delta);
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		_currentState.HandleInput(@event);
	}
	
	public void TransitionTo(string key)
	{
		if(!_states.ContainsKey(key) || _currentState == _states[key])
		{
			return;
		}
		
		GD.Print("Transitioning from ", _currentState.Name.ToString(), " To ",
		 _states[key].Name.ToString());
		
		_currentState.Exit();
		_currentState = _states[key];
		_currentState.Enter();
	}
}
```
Ez itt a "StateMachine" osztályunk. Ő fel lesz használva mint futtatható script, ő lesz nekünk az állapotgépünk gyökere, minden más Node, ami állapot, hozzá fog tartozni. Itt sok minden történik, de leegyszerűsítve az "initialState" változónk, egy "NodePath", avagy Node-hoz vezető út. Az \[Export\], ami elé van írva egyszerűen annyit jelent, hogy az editor-ból is meg tudjuk változtatni annak értékét.
			![[Pasted image 20260121153334.png]] ![[Pasted image 20260121153325.png]] 
Az állapotgépünknek minden képpen kell egy kiindulópont, egy első állapot és ezt mi választjuk ki itt. Továbbá egy Dictionary-ként tárolom a helyes állapotokat és lementem egy "\_currentState" változóba a jelenlegi állapotot, hogy mindig számon tudjam tartani.
A \_Ready() metódusban, ahogyan látjuk végigiterálunk minden "Node" típusú változón. Itt térek vissza az előbbi kijelentésemre, muszáj volt megtartani a "State" osztályt Node-ként, mivel a "GetChildren()" beépített metódus Node típusú változókat ad vissza. A \_Process(), \_PhysicsProcess() és \_UnhandledInput() metódusok mind meghívják a jelenlegi állapot saját metódusát ezekre az alkalmakra. Végül a TransitionTo(string key) metódus felelős az állapotok közötti váltásért. A "kulcs" ebben az esetben nem más mint egy string, ami magának a Node-nak is a neve. Ez a jobb olvashatóság és struktúra érdekében volt így megoldva.

Példaképpen:
![[Pasted image 20260216181449.png]]
Az állapotgépünk áll négy állapotból. Mindegyik állapothoz hozzá van rendelve egy fényforrás és egy időzítő.
```C#
public partial class State3 : State
{
	public override void Enter()
	{
		GetNode<Node3D>("Light").Visible = true;
		GetNode<Timer>("Timer").Start();
	}
	
	public override void HandleInput(InputEvent @event)
	{
		if(@event is InputEventKey eventKey)
		{
			if(eventKey.Pressed)
			{
				if(eventKey.Keycode == Key.Key1)
				{
					EmitSignal(SignalName.Transition, "State1");
				}
				if(eventKey.Keycode == Key.Key2)
				{
					EmitSignal(SignalName.Transition, "State2");
				}
				if(eventKey.Keycode == Key.Key3)
				{
					EmitSignal(SignalName.Transition, "State3");
				}
				if(eventKey.Keycode == Key.Ctrl)
				{
					EmitSignal(SignalName.Transition, "State4");
				}
			}
		}
	}
	
	public override void Exit()
	{
		GetNode<Node3D>("Light").Visible = false;
		GetNode<Timer>("Timer").Stop();
	}
	
	private void _OnTimerTimeout()
	{
		EmitSignal(SignalName.Transition, "State1");
	}
}
```
A "State3"-nak elnevezett állapotban vagyunk jelenleg, ami ebben az esetben a "Piros" állapot a jelzőlámpánkon. Amikor erre az állapotra váltunk, akkor a State3-hoz csatolt fényforrást láthatóvá kell tennünk, valamint el kell indítanunk az időzítőnket, hogy nehogy benne ragadjunk. Amikor az időzítő lejár átmegyünk a "State1"-ba, ami a mi estünkben a "Zöld". *Amikor* átlépünk a következő állapotba fontos, hogy az előző állapotból sikeresen kilépjünk és megszüntessünk minden olyan dolgot, ami bezavarna más állapotok működésébe. Ezért az állapotgépünk meg fogja hívni a State3-nak az "Exit()" metódusát, amelyben is elrejtjük a fényforrását és megállítjuk az időzítőjét, arra az esetre, ha nem állna meg magától.
Ezen felül van egy egyszerű switch statement, ami felhasználói bemenetre reagál ezzel lehetővé téve az állapotok közötti egyszerű ugrálást.