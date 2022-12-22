import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
const PersonProfile = Loadable(lazy(() => import('../views/Person/Profile')));
const PersonEdit = Loadable(lazy(() => import('../views/Person/Edit')));
// const PersonCreate = Loadable(lazy(() => import('../views/Person/Create')));
const CreateAccessProgram = Loadable(lazy(() => import('../views/AccessProgram/Create')));
const IndexAccessProgram = Loadable(lazy(() => import('../views/AccessProgram')));

const AccessProgramRoute = {
    path: '/access-program',
    element: <MainLayout/>,
    children: [
        {
            path: '/access-program',
            element: <IndexAccessProgram/>
        },
        {
            path: '/access-program/:id',
            element: <PersonProfile/>
        },
        {
            path: '/access-program/create',
            element: <CreateAccessProgram/>
        },
        {
            path: '/access-program/:id/edit',
            element: <PersonEdit/>
        },
    ]
 };

export default AccessProgramRoute;
