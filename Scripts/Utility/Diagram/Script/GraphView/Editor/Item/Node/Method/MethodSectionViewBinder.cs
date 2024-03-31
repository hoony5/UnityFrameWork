using System;

namespace Diagram
{
    public class MethodSectionViewBinder
    {
        private MethodSectionView view;
        
        public MethodSectionViewBinder(MethodSectionView view)
        {
            this.view = view;
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