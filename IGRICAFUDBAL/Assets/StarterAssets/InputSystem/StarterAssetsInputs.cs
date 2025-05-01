using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Vrednosti za unos karaktera")]
        public Vector2 move; // Vektor koji predstavlja unos za kretanje
        public Vector2 look; // Vektor koji predstavlja unos za gledanje (rotiranje kamere)
        public bool jump; // Promenljiva koja oznacava unos za skok
        public bool sprint; // Promenljiva koja oznacava unos za sprint
        public bool shoot; // Promenljiva koja oznacava unos za sut

        [Header("Podešavanja za kretanje")]
        public bool analogMovement; // Promenljiva koja oznacava da li je unos kretanja analogni

        [Header("Podešavanja za miša kursor")]
        public bool cursorLocked = true; // Promenljiva koja oznacava da li je kursor zakljucan
        public bool cursorInputForLook = true; // Promenljiva koja oznacava da li se unos za rotiranje kamere vrsi putem misa

#if ENABLE_INPUT_SYSTEM 
        // Metoda koja se poziva prilikom unosa za kretanje
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        // Metoda koja se poziva prilikom unosa za rotiranje kamere (ukoliko je omoguceno putem misa)
        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        // Metoda koja se poziva prilikom unosa za skok
        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        // Metoda koja se poziva prilikom unosa za pucanje
        public void OnShoot(InputValue value)
        {
            ShootInput(value.isPressed);
        }

        // Metoda koja se poziva prilikom unosa za sprint
        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
#endif

        // Metoda koja postavlja vrednost za unos kretanja
        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        // Metoda koja postavlja vrednost za unos rotiranja kamere
        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        // Metoda koja postavlja vrednost za unos skoka
        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        // Metoda koja postavlja vrednost za unos pucanja
        public void ShootInput(bool newShootState)
        {
            shoot = newShootState;
        }

        // Metoda koja postavlja vrednost za unos sprinta
        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        // Metoda koja se poziva kada aplikacija dobije ili izgubi fokus
        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        // Metoda koja postavlja stanje kursora (zakljucan ili otkljucan)
        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
