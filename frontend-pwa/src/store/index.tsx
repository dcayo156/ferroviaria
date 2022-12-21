import { configureStore,ConfigureStoreOptions } from '@reduxjs/toolkit'
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import { personApi} from './services/Person'
import {authApi} from './services/Auth'
import authReducer from './slices/Auth'
import { accessProgramApi} from './services/AccessProgram'

export const createStore = (options?:ConfigureStoreOptions['preloadedState'] | undefined) => 
    configureStore({
        reducer:{
            [personApi.reducerPath]: personApi.reducer,
            [authApi.reducerPath]: authApi.reducer,
            [accessProgramApi.reducerPath]: accessProgramApi.reducer,
            auth: authReducer,
        },
        middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(
                                                personApi.middleware,
                                                accessProgramApi.middleware,
                                                authApi.middleware
                                                ),
        ...options,
    })


export const store = createStore();
export type AppDispatch = typeof store.dispatch;
export const useAppDispatch = () => useDispatch<AppDispatch>();
export type RootState = ReturnType<typeof store.getState>;
export const useTypedSelector: TypedUseSelectorHook<RootState>=useSelector;