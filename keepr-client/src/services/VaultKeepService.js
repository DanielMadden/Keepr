import { AppState } from '../AppState'
import { api } from './AxiosService'

const baseURL = '/api/vaultkeeps/'

class VaultKeepService {
  async create(newVK) {
    await api.post(baseURL, newVK)
  }

  async delete(id) {
    AppState.activeVaultKeeps = AppState.activeVaultKeeps.filter(keep => keep.vaultKeepId !== id)
    await api.delete(baseURL + id)
  }
}

export const vaultKeepService = new VaultKeepService()
