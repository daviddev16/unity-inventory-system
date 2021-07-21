using System.Collections.Generic;

public interface AbstractInventory
{
    List<DefaultContainer> Containers { get; set; }

    void AddItem(AbstractItem Item);
    
    void RemoveItem(AbstractItem Item);

    bool ContainsItem(AbstractItem item);

}
