**State machines serve as fundamental models for understanding and describing the behaviour of systems in computer [science](https://softwaredominos.com/home/science-technology-and-other-fascinating-topics/).** 

~~At their core, state machines provide a structured approach to representing a system’s various states, the transitions between them, and the events triggering them.

~~Finite State Machines are also **an essential component for understanding Universal [Turing](https://softwaredominos.com/home/software-engineering-and-computer-science/alan-turing-and-the-turing-machine-the-foundation-of-modern-computing/) Machines (UTM), as the latter consists of two parts: an FSM and an infinite tape.**


**A state machine is a mathematical model that represents the dynamic behaviour of a system through a finite number of states, transitions between these states, and actions associated with these transitions. At any given moment, a system is in a particular state, and transitions occur in response to events or inputs.**

Finite State Machines can be categorized as deterministic or nondeterministic based on their behaviour:

- **Deterministic FSMs:** In deterministic FSMs, every state has a unique transition for each possible input. The next state is determined solely by the current state and the input received, leading to predictable and unambiguous behaviour.

- **Nondeterministic FSMs:** In nondeterministic FSMs, multiple transitions may exist for a given input and current state. The system’s subsequent state is not uniquely determined, allowing for more flexibility but also introducing ambiguity and [complexity](https://softwaredominos.com/home/science-technology-and-other-fascinating-topics/complexity-in-natural-and-human-systems-why-and-when-we-should-care/).


State machines consist of several key components:

**States:**

- States represent the various conditions or modes a system can be in at any given time. Each state defines a unique set of behaviours or characteristics the system exhibits. The number of states in our machine is finite.

- In the diagram above, the FSM has two states, Q1 and Q2. An example of such a machine would be an electronic gate with open and closed states.

**Transitions:**

- Transitions describe the system’s movement from one state to another in response to events or inputs. Specific events or stimuli trigger transitions.

- In our electronic gate example, the transitions could occur due to a controller input current, which would cause the gate to close or open.

**Stimuli**:

- Stimuli are external events or inputs that trigger state transitions. They can be user interactions, sensor readings, network messages, or any other input that causes the system to change its state.

- In our basic example above, the stimuli represent the outputs of the controller device, which could be positive to induce the gate to open or negative to force it to close.

**Responses:**

- Activities or operations performed when transitioning between states. Responses may include updating variables, executing [algorithms](https://softwaredominos.com/home/software-engineering-and-computer-science/top-15-algorithms-every-software-engineer-must-know/), sending messages, or any other task necessary to maintain the system’s behaviour.

- R1, R2 and R3 represent the responses of our state machine to stimuli S1, S2 and S3. These could be voltages on an output wire.

