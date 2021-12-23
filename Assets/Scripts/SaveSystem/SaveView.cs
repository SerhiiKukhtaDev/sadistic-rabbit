using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SaveSystem
{
    public class SaveView : MonoBehaviour
    {
        [SerializeField] private Text changeMessage;
        [SerializeField] private Text changeDate;
        [SerializeField] private Text scoreText;
        [SerializeField] private Button returnToChangeButton;

        private ChangeEntity _changeEntity;
        
        public UnityAction<ChangeEntity, SaveView> ReturnToChange { get; set; }

        private void OnDisable()
        {
            returnToChangeButton.onClick.RemoveListener(OnChangeButtonClicked);
        }

        public void Render(ChangeEntity change)
        {
            changeMessage.text = change.Message;
            changeDate.text = change.Date.ToString("HH:mm:ss");
            scoreText.text = change.PlayerStateEntity.Score.ToString();
            returnToChangeButton.onClick.AddListener(OnChangeButtonClicked);
            
            _changeEntity = change;
        }

        private void OnChangeButtonClicked()
        {
            ReturnToChange?.Invoke(_changeEntity, this);
            // var data = SessionsDatabase.GetPlayerInfo(_changeEntity.ID);
            //
            // PlayerData.Instance.Score = data.score;
            // PlayerData.Instance.CurrentWeaponId = data.current_weapon_id;
            // PlayerData.Instance.UnlockedLevels = data.levels.ToList();
            // PlayerData.Instance.WeaponsId = data.weapons.ToList();
            //
            // SessionsDatabase.DeleteAllChangesExcept(_changeEntity.ID);
        }
    }
}
