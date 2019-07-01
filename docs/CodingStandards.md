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


### General Naming ###

**DO** name methods that assert some condition as being true as *CheckXxxx*, where *Xxxx* is the condition
being checked.

**CONSIDER** making pre-flight checks that are potentially common use into *CheckXxxx* methods.


### Stacks ###

Names get a little hairy here.  In the vernacular of the project, a *Deck* is the entire ecosystem
of a set of play components, which consists of at least a *DrawPile* and *DiscardPile*.  Other possible 
areas may exist and several have been put out.  

**DO** derive your stacks from *DeckStack*, this implements the base capabilities all such areas should have
and provides expected commonly used features.

**DO** add an internal accessor to your stack named *XxxxStack*.

**DO** add a public accessor to your stack named *Xxxx*, for the same value of *Xxxx*.