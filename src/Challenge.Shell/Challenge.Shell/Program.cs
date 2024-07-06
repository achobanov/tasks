using Challenge.Domain;
using Challenge.Console;
using Challenge.Domain.Services;

var randomProvider = new RandomProvider();
var outcomeFactory = new BetOutcomeFactory(randomProvider);
var loss = outcomeFactory.Loss(50);
var doubleX = outcomeFactory.Win(40, 1.01f, 2);
var tenX = outcomeFactory.Win(10, 2, 10);
var slotMachine = new SlotMachine(randomProvider, 1, 10, loss, doubleX, tenX);

var integration = new Integration(new ConsoleShell());
integration.Activate(slotMachine);
integration.Select(slotMachine.Name);
integration.Start();