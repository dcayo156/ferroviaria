import * as React from 'react';
import { useTheme } from '@mui/material/styles';
import { Card, CardContent, CardHeader, Divider, Typography,Color } from '@mui/material';


const headerSX = {
    '& .MuiCardHeader-action': { mr: 0 }
};


interface IContent{
    children : any
    secondary: any
    title: any
}
 
const MainCard: React.FunctionComponent<IContent> = ({
    children,
    secondary,
    title,
}) => {
    const theme = useTheme();
    return ( <Card   
        sx={{
            borderColor: ((theme.palette.primary as unknown) as Color)[500],
            ':hover': {
                boxShadow: '0 2px 14px 0 rgb(32 40 45 / 8%)'
            },
        }}
    >
        {title && (
            <CardHeader sx={headerSX} title={<Typography variant="h4">{title}</Typography>} action={secondary} />
        )}
        {title && <Divider />}
            <CardContent >
                {children}
            </CardContent>
        
    </Card> );
}
 
export default MainCard;
