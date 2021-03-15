import ClaimPrincipal, { claimResource } from '../../../../modules/claim';

export const hasWriteAdminRoleClaim = ClaimPrincipal.currentClaimPrincipal.hasWriteClaim(claimResource.AdminRoleMenu);