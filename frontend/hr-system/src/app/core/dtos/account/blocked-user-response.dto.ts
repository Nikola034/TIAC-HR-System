export interface BlockedUserResponseDto{
    id: string,
    email: string,
    refreshToken: string,
    passwordResetToken: string,
    isBlocked: boolean
}