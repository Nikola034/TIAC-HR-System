export interface Account{
    id: string,
    email: string,
    refreshToken: string,
    passwordResetToken: string,
    isBlocked: boolean
}
