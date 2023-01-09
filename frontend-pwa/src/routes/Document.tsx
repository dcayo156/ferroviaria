import { lazy } from 'react';
import MainLayout from '../layout/MainLayout';
import Loadable from '../components/LoadComponent/Loadable';
// const PersonCreate = Loadable(lazy(() => import('../views/Person/Create')));
const CreateDocument = Loadable(lazy(() => import('../views/Document/Create')));
const IndexDocument = Loadable(lazy(() => import('../views/Document')));
const IndexDocumentEdit = Loadable(lazy(() => import('../views/Document/Edit')));

const DocumentRoute = {
    path: '/document',
    element: <MainLayout/>,
    children: [
        {
            path: '/document',
            element: <IndexDocument/>
        },
        {
            path: '/document/create',
            element: <CreateDocument/>
        },
        {
            path: '/document/:id/edit',
            element: <IndexDocumentEdit/>
        },
    ]
 };

export default DocumentRoute;
