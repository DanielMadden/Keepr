import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  loggingIn: false,
  user: {},
  account: {},
  activeUserVaults: [],
  activeProfile: null, // {}
  activeProfileVaults: [],
  activeProfileKeeps: [],
  activeKeep: {},
  activeVault: null, // {}
  activeVaultKeeps: {},
  homeKeeps: [],
  searchResults: [],
  deleting: {
    keep: false,
    vault: false
  },
  loaded: {
    home: false,
    profileVaults: false,
    profileKeeps: false
  },
  animating: {
    home: false,
    vault: false
  },
  authorized: {
    vault: false
  },
  authorizing: {
    vault: true
  },
  isPrivate: {
    vault: false
  },
  opening: {
    vault: false
  }
})
