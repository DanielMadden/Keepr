import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  activeUserVaults: [],
  activeProfile: null,
  activeProfileVaults: [],
  activeProfileKeeps: [],
  activeKeep: {},
  activeVault: {},
  activeVaultKeeps: {},
  homeKeeps: [],
  searchResults: []
})
