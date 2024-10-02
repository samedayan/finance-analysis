export interface PartnerDto {
  id: string;
  name?: string;
  statusName?: string;
  createdDate: Date;
  contactEmail?: string;
  contactPhone?: string;
}

export interface PartnerForUpdateDto {
  name?: string;
  contactEmail?: string;
  contactPhone?: string;
}

export interface PartnerForCreationDto {
  name?: string;
  contactEmail?: string;
  contactPhone?: string;
}

export interface WorkItemDto {
  id: string;
  name?: string;
  createdDate: Date;
  agreement?: PartnerAgreementDto | null;
  riskAnalysis: RiskAnalysisDto | null;
}

export interface RiskAnalysisDto {
  workItemId?: string;
  calculatedRisk?: string;
  workItem?: WorkItemDto | null;
}

export interface PartnerAgreementDto {
  id: string;
  name?: string;
  partner?: PartnerDto | null;
  createdDate: Date;
  workItems?: WorkItemDto[];
}
