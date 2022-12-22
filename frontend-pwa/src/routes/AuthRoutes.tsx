import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
const ChangePassword = Loadable(lazy(() => import('../views/Auth/ChangePassword')));
const ShowUser = Loadable(lazy(() => import('../views/Auth/ShowUser')));
const EditUser = Loadable(lazy(() => import('../views/Auth/EditUser')));
const AuthRoutes = {
    path: '/auth',
    element: <MainLayout/>,
    children: [
        {
            path: '/auth/change-password/:id',
            element: <ChangePassword/>
        },
        {
            path: '/auth/show/user-authenticated',
            element: <ShowUser/>
        },
        {
            path: '/auth/edit/:id',
            element: <EditUser/>
        },
    ]
 };

export default AuthRoutes;
