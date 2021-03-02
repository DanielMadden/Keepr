import { AppState } from '../AppState'
import { api } from './AxiosService'

const baseURL = '/api/vaults/'

class VaultService {
  async getVault(id) {
    const res = await api.get(baseURL + id)
    AppState.activeVault = res.data
  }

  async getKeeps(id) {
    const res = await api.get(baseURL + id + '/keeps')
    AppState.activeVaultKeeps = res.data
  }

  async create(newVault) {
    await api.post(baseURL, newVault)
  }

  async edit(newVault) {
    await api.put(baseURL + newVault.id, newVault)
  }

  async delete(id) {
    await api.delete(baseURL + id)
  }
}

export const vaultService = new VaultService()
