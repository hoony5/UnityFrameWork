using Sirenix.OdinInspector;
using Unity.Properties;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ViewModelTest : MonoBehaviour
{
     public UIDocument document;
     [SerializeField] private Player player;
     [SerializeField] private ViewModel<string> playerNameViewModel;
     [SerializeField] private ViewModel<Player> playerViewModel;
     private ProgressBar hpBar;
     private Label nameLabel;

     [Button]
     public void Print()
     {
          Debug.Log(nameof(ViewModel<Player>.Value.hp));
     }
     [Button]
     public void Bind()
     {
          // SetUp ViewModel
          playerNameViewModel = new ViewModel<string>();
          playerViewModel = new ViewModel<Player>();
          player = new Player();
          
          // SetUp UI Elements
          hpBar = new ProgressBar();
          nameLabel = new Label();
          
          // Bind UI Elements to ViewModel
          //hpBar.Bind(player, nameof(Player.hp));
          hpBar.Bind(playerViewModel, nameof(Player.hp));
          nameLabel.Bind(playerViewModel, nameof(Player.name));
          
          // display
          VisualElement root = new VisualElement();
          root.Add(hpBar);
          root.Add(nameLabel);
          
          root.style.flexDirection = FlexDirection.Column;
          
          document.rootVisualElement.Add(root);
     }
     [Button]
     public void Increment()
     {
          hpBar.value += 10;
     }
     
     [Button]
     public void Decrement()
     {
          hpBar.value -= 10;
     }
     
     [Button]
     public void Release()
     {
          hpBar?.ClearBindings();
          nameLabel?.ClearBindings();
          document.rootVisualElement?.Clear();
     }
     [System.Serializable]
     public class Player
     {
          public float hp;
          public string name;

          public Player() { }
          public Player(ViewModel<Player> viewModel)
          {
               hp = viewModel.Value.hp;
               name = viewModel.Value.name;
          }
     }
}
