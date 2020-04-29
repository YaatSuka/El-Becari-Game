using UnityEngine;

using Interactable;

public class SlotController
{
    public int length;

    private Slot[] slots;

    public SlotController(Value[] values)
    {
        this.length = values.Length;
        this.slots = new Slot[values.Length];

        foreach(Value value in values) {
            //TO DO: Compute position
            this.slots[value.uid] = new Slot(new Vector2(0, 0), value);
        }
    }
}