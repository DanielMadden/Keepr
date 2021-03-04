import { AppState } from '../AppState'
import { api } from './AxiosService'
import { profileService } from './ProfileService'

const baseURL = '/api/vaults/'

class VaultService {
  async getVault(id) {
    try {
      const res = await api.get(baseURL + id)
      AppState.authorized.vault = true
      AppState.activeVault = res.data
      AppState.opening.vault = true
      setTimeout(() => { AppState.animating.vault = false; AppState.opening.vault = false }, 1000)
    } catch (error) {
      AppState.authorized.vault = false
    }
  }

  async getKeeps(id) {
    const res = await api.get(baseURL + id + '/keeps')
    AppState.activeVaultKeeps = res.data
  }

  async create(newVault) {
    await api.post(baseURL, newVault)
    profileService.getVaults(AppState.account.id)
  }

  async edit(newVault) {
    await api.put(baseURL + newVault.id, newVault)
  }

  async delete(id) {
    await api.delete(baseURL + id)
    return 'deleted'
  }
}

export const vaultService = new VaultService()
