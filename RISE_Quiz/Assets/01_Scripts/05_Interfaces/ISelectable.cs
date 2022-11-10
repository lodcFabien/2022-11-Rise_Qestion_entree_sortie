using System;

public interface ISelectable
{
    void SelectWithNotify();
    void DeselectWithNotify();
}