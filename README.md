This program calculates how many rounds it takes for a Menu of items to be cooked on a grill.

Due to some ambiguity in the requirements, I've calculated this two ways:

1 - Under the assumption that the items can be cut up, therefore maximising the entire space of the grill, filling every square cm

2 - What I imagined was expected from the requirements, using a 2d Bin Packing algorithm, breaking the rectangular grill down into smaller rectangles, and representing the items as rectangles.
    I've done this in order by size, so the algorithm will always try to place the largest items on the grill first
