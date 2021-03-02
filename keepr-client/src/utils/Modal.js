import { reactive } from 'vue'

// Modal is a reactive object for deciding when to show modals, when to close, and what to hide/show
export const ModalState = reactive({
  darken: false,
  show: false,
  active_keep: false,
  active_add_vault: false,
  active_add_keep: false
})

// openModal() takes in a string that is the name of the desired modal
export function openModal(modalChoice) {
  modalChoice = 'active_' + modalChoice
  ModalState.darken = true
  ModalState.show = true
  ModalState[modalChoice] = true
}

// closeModals() cycles through all properties and sets them to false
export const closeModals = () => {
  const keyVal = Object.keys(ModalState)
  keyVal.forEach(key => {
    if (key !== 'd' || key !== 's' || key !== 'a') {
      ModalState[key] = false
    }
  })
}
