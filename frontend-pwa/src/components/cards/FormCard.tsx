import { useTheme } from '@mui/material/styles';
import { CardContent, CardHeader, Avatar, Card, SvgIconTypeMap } from '@mui/material';

import * as React from 'react';

interface FormCardProps {
    icon: JSX.Element
    title: string
    action: JSX.Element | null
    style: React.CSSProperties | undefined
    children:JSX.Element
}

const FormCard: React.FunctionComponent<FormCardProps> = ({ icon, title,action, style,children  }) => {
    const theme=useTheme();
    return (<Card style={{ ...style,  boxShadow: `1px 0px 3px 0px ${theme.palette.secondary.main}`,width:"100%" }}>
        <CardHeader
            avatar={
                <Avatar>
                    {icon}
                </Avatar>
            }
            title={title}
            action={
                action && action
            }
        />
        <CardContent>
            {children}
        </CardContent>
    </Card>);
}

export default FormCard;