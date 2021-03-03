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
  ModalState.darken = true
  ModalState.show = true
  ModalState['active_' + modalChoice] = true
}

// closeModals() cycles through all properties of the ModalState and sets them to false
export const closeModals = () => {
  const keyVal = Object.keys(ModalState)
  keyVal.forEach(key => {
    // There are "d" "s" and "a" properties on reactive objects. Ignore them.
    if (key !== 'd' || key !== 's' || key !== 'a') {
      ModalState[key] = false
    }
  })
}
