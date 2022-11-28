import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import type { LoginResponse} from  '../../types/Auth'
import type { RootState } from '../..'
import { authenticate, getCredentials } from './localStorage'

const slice = createSlice({
  name: 'auth',
  initialState: getCredentials() as LoginResponse,
  reducers: {
    setCredentials: (
      state,
      { payload: payload }: PayloadAction<LoginResponse>
    ) => {
      state.id = payload.id
      state.username = payload.username
      state.email = payload.email
      state.token = payload.token
      state.refreshToken = payload.refreshToken
      authenticate(payload);
    },
  },
})

export const { setCredentials } = slice.actions

export default slice.reducer

export const selectCurrentUser = (state: RootState) => state.auth.username
