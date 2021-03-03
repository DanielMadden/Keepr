import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      AppState.loggingIn = true
      const res = await api.get('/api/account')
      res.data.name = res.data.name.split('@')[0]
      AppState.account = res.data
      AppState.loggingIn = false
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
