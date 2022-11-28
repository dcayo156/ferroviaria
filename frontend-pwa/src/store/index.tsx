import { configureStore,ConfigureStoreOptions } from '@reduxjs/toolkit'
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import { personApi} from './services/Person'
import { tagApi } from './services/Tag'
import {addressApi} from './services/Address'
import {authApi} from './services/Auth'
import authReducer from './slices/Auth'
import { relationshipApi } from './services/Relationship';

export const createStore = (options?:ConfigureStoreOptions['preloadedState'] | undefined) => 
    configureStore({
        reducer:{
            [personApi.reducerPath]: personApi.reducer,
            [addressApi.reducerPath]: addressApi.reducer,
            [tagApi.reducerPath]: tagApi.reducer,
            [relationshipApi.reducerPath] : relationshipApi.reducer,
            [authApi.reducerPath]: authApi.reducer,
            auth: authReducer,
        },
        middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(
                                                personApi.middleware,
                                                tagApi.middleware,
                                                addressApi.middleware,
                                                relationshipApi.middleware,
                                                authApi.middleware
                                                ),
        ...options,
    })


export const store = createStore();
export type AppDispatch = typeof store.dispatch;
export const useAppDispatch = () => useDispatch<AppDispatch>();
export type RootState = ReturnType<typeof store.getState>;
export const useTypedSelector: TypedUseSelectorHook<RootState>=useSelector;