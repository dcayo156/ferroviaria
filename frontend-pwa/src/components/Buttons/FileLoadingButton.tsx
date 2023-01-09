import { Box, Button, CircularProgress, useTheme } from '@mui/material';
import * as React from 'react';
interface LoadingButtonProps {
    loading:boolean
    text:string
}
 
const FileLoadingButton: React.FunctionComponent<LoadingButtonProps> = ({loading,text}) => {
    const theme=useTheme();
    return ( <Box sx={{ m: 1, position: 'relative' }}>
    <Button
      variant="outlined"
      component="span"
      disabled={loading}
    >
      {text}
    </Button>
    {loading && (
      <CircularProgress
        size={24}
        sx={{
          color: theme.palette.primary.main,
          position: 'absolute',
          top: '50%',
          left: '50%',
          marginTop: '-12px',
          marginLeft: '-12px',
        }}
      />
    )}
  </Box> );
}
 
export default FileLoadingButton;