import ClaimPrincipal, { claimResource } from '../../../../modules/claim';

export const hasWriteUserControlClaim = ClaimPrincipal.currentClaimPrincipal.hasWriteClaim(claimResource.UserControl);