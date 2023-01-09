import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
// const PersonCreate = Loadable(lazy(() => import('../views/Person/Create')));
const CreateAccessProgram = Loadable(lazy(() => import('../views/AccessProgram/Create')));
const IndexAccessProgram = Loadable(lazy(() => import('../views/AccessProgram')));
const IndexAccessProgramEdit = Loadable(lazy(() => import('../views/AccessProgram/Edit')));

const AccessProgramRoute = {
    path: '/access-program',
    element: <MainLayout/>,
    children: [
        {
            path: '/access-program',
            element: <IndexAccessProgram/>
        },
        {
            path: '/access-program/create',
            element: <CreateAccessProgram/>
        },
        {
            path: '/access-program/:id/edit',
            element: <IndexAccessProgramEdit/>
        },
    ]
 };

export default AccessProgramRoute;
