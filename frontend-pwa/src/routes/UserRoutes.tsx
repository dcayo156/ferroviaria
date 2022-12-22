import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
const IndexUser = Loadable(lazy(() => import('../views/User')));
const UserCreate = Loadable(lazy(() => import('../views/User/Create')));
const UserEdit = Loadable(lazy(() => import('../views/User/Edit')));
const UserRoutes = {
    path: '/user',
    element: <MainLayout/>,
    children: [
        {
            path: '/user',
            element: <IndexUser/>
        },
        {
            path: '/user/create',
            element: <UserCreate/>
        },
        {
            path: '/user/:id/edit',
            element: <UserEdit/>
        },
    ]
 };

export default UserRoutes;
