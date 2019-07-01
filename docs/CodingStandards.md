## Coding Standards ##

The following is a guide for people looking to contribute to the Decks project.

### Golden Rule ###
When not otherwise expressly called out below, try to make the code follow what already 
exists as much as possible.

### Documentation Standards ###

**DO** document all public methods on types and interfaces.

**DO** document all internal methods on types and interfaces.

**DO** document all protected methods on types and interfaces.

**DO** document all protected internal methods on types and interfaces.

**CONSIDER** documenting any private method.

**DO** document all enum types and members.

### General ###

**CONSIDER** returning only interfaces.

**CONSIDER** having properties be read-only when managed internally.

**CONSIDER** having returned values be read-only interfaces (IReadOnlyCollection<>,
IEnumerable<>, etc.) whenever it's not inteded for the user to modify a collection.

**CONSIDER** making all collections observable.   This really helps when used in a rich-client UX.

### General Naming ###

**DO** name methods that assert some condition as being true as *CheckXxxx*, where *Xxxx* is the condition
being checked.

**CONSIDER** making pre-flight checks that are potentially common use into *CheckXxxx* methods.

**DO** use **TElement** as the name of the type of things that are stored in the 
deck and it's various components throughout.  When talking about the parts of any 
of these piles, they should be called "elements" (not cards, etc.).  

>	**EXCEPTION** When discussing a sample deck that actually contains cards, call 
>   them cards.

### Stacks ###

Names get a little hairy here.  In the vernacular of the project, a *Deck* is the entire ecosystem
of a set of play components, which consists of at least a *DrawPile* and *DiscardPile*.  Other possible 
areas may exist and several have been put out.  Each area that is a collection of elements of the type of
the *Deck* is  *DeckStack*.

**DO** derive your stacks from *DeckStack*, this implement the base capabilities all such areas should have
and provides expected commonly used features.

**DO** add an internal accessor to your stack named *XxxxStack*.

**DO** add a public accessor to your stack named *Xxxx*, for the same value of *Xxxx*.

**DO** add an interface for your custom deck stack.

**DO** derive that interface off of IDeckStack<T>.

**DO** make the implementation of your deck stack derive from *DeckStack&lt;T&gt;* as an *internal* class.

**DO NOT** create a namespace for your deck stack.

**DO** create a directory for your deck stack.

### Non-Stack Collections ###

Sometimes we have collections of things that are not, themselves, deck stacks (hands comes to mine).
In these cases, follow the following rules.

**DO** add an internal accessor to your collection named *XxxxSet*.

**DO** add a public accessor to your stack named *Xxxx*, for the same value of *Xxxx*.
