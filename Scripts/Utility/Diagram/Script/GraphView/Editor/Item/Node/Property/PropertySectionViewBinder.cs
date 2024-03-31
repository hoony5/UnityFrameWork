using System;

namespace Diagram
{
    public class PropertySectionViewBinder
    {
        private PropertySectionView view;

        public PropertySectionViewBinder(PropertySectionView propertySectionView)
        {
            view = propertySectionView;
        }
        public void BindAddButton(Action action)
        {
            if(view.addButton == null) return;
            view.addButton.clicked += action;
        }
        public void DisposeAddButton(Action action)
        {
            if(view.addButton == null) return;
            view.addButton.clicked -= action;
        }
    }

}