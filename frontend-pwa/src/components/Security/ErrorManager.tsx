import { FetchBaseQueryError } from '@reduxjs/toolkit/query'
import { SerializedError } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';

function isFetchBaseQueryError(
    error: unknown
): error is FetchBaseQueryError {
    return typeof error === 'object' && error != null && 'status' in error
}

function isErrorWithMessage(
    error: unknown
): error is { message: string } {
    return (
        typeof error === 'object' &&
        error != null &&
        'message' in error &&
        typeof (error as any).message === 'string'
    )
}

const showError = (err: { data: any } | { error: FetchBaseQueryError | SerializedError }| FetchBaseQueryError | SerializedError | undefined, message: string) => {
    let errMsg: string | FetchBaseQueryError | SerializedError | ((FetchBaseQueryError | SerializedError) & string) = ""
    if (isFetchBaseQueryError(err)) {
        errMsg = 'error' in err ? err.error : JSON.stringify(err.data)
    } else if (isErrorWithMessage(err)) {
        errMsg = err.message;
        message = err.message;
    }
    console.error(errMsg);
    toast.error(message);
}
export const hasError = (err: { data: any } | { error: FetchBaseQueryError | SerializedError } | FetchBaseQueryError | SerializedError | undefined, message?: string) => {
    let has_error = false;
    if (isFetchBaseQueryError(err) || isErrorWithMessage(err)) {
        has_error = true;
        showError(err, message === undefined ? "Error!!! algo salio mal" : message);
    }
    return has_error;
}