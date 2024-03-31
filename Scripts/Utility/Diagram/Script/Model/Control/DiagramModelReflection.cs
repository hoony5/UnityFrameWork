using System;
using System.Linq;
using Share;

namespace Diagram
{
    public class DiagramModelReflection
    {
        private DiagramNodeModel _nodeModel;

        public DiagramModelReflection(DiagramNodeModel nodeModel)
        {
            this._nodeModel = nodeModel;
        }

        #region Reflection

        private string GetPropertyParentPath(Type type, string name)
        {
            if (_nodeModel.Inheritances.Count == 0) return string.Empty;
            foreach (DiagramHeaderModel inheritance in _nodeModel.Inheritances)
            {
                int parentPropertyIndex =
                    inheritance.Properties.FindIndex(item => item.Type.Equals(type.Name) && item.Name.Equals(name));
                if (parentPropertyIndex == -1) continue;
                return inheritance.Name;
            }

            return string.Empty;
        }

        private string GetMethodParentPath(Type type, string name)
        {
            if (_nodeModel.Inheritances.Count == 0) return string.Empty;
            foreach (DiagramHeaderModel inheritance in _nodeModel.Inheritances)
            {
                int parentMethodIndex =
                    inheritance.Methods.FindIndex(item => item.Type.Equals(type.Name) && item.Name.Equals(name));
                if (parentMethodIndex == -1) continue;
                return inheritance.Name;
            }

            return string.Empty;
        }

        public string GetPropertyPath(Type type, string name)
        {
            if (_nodeModel.Header.Properties.Count == 0) return string.Empty;
            string target = _nodeModel.Header.Properties.FirstOrDefault(p => p.Type.Equals(type.Name) && p.Name.Equals(name))?.Name;
            string parentPath = GetPropertyParentPath(type, name);

            return
                $"{(target.IsNullOrEmpty() ? "" : parentPath.IsNullOrEmpty() ? $"{_nodeModel.Header.Name}.{target}" : $"{parentPath}.{target}")}";
        }

        public string GetMethodPath(Type returnType, string name)
        {
            if (_nodeModel.Header.Methods.Count == 0) return string.Empty;
            string target = _nodeModel.Header.Methods.FirstOrDefault(m => m.Type.Equals(returnType.Name) && m.Name.Equals(name))?.Name;
            string parentPath = GetMethodParentPath(returnType, name);

            return
                $"{(target.IsNullOrEmpty() ? "" : parentPath.IsNullOrEmpty() ? $"{_nodeModel.Header.Name}.{target}" : $"{parentPath}.{target}")}";
        }

        public bool HasNamespace()
        {
            return !string.IsNullOrEmpty(_nodeModel.Header.Namespace) || !_nodeModel.Header.Namespace.Equals("None", StringComparison.OrdinalIgnoreCase);
        }

        #endregion

    }

}