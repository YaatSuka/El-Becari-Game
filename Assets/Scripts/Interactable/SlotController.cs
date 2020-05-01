using UnityEngine;

using Interactable;

public class SlotController
{
    public int length;

    private Slot[] slots;

    public SlotController(Value[] values)
    {
        int uid = 0;
        this.length = values.Length;
        this.slots = new Slot[values.Length];

        foreach(Value value in values) {
            //TO DO: Compute position
            Vector2 position = new Vector2(0, 0);

            this.slots[value.uid] = new Slot(position, uid, value);
            uid++;
        }
    }
}