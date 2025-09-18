import React from 'react';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { resolveClientUrl, formatDateTime } from '../../../../modules/utils';
import StatusDropdown from '../components/StatusDropdown';
import IframeHoverPreview from '../../page-management/components/IframeHoverPreview';

/**
 * Page Management Content Configuration
 */
export const pageManagementContentConfig = (props) => [
    {
        key: 'index',
        title: '#',
        width: 10,
        className: 'text-center',
        render: (dataItem, index) => index + 1
    },
    {
        key: 'campaignCode',
        title: 'Campaign Code',
        width: 50,
        className: 'text-left',
        render: (dataItem) => htmlDecode(dataItem.CampaignCode),
        getTitle: (dataItem) => htmlDecode(dataItem.CampaignCode)
    },
    {
        key: 'campaignName',
        title: 'Campaign Name',
        width: 60,
        className: 'text-left',
        render: (dataItem) => htmlDecode(dataItem.CampaignName),
        getTitle: (dataItem) => htmlDecode(dataItem.CampaignName)
    },
    {
        key: 'pageName',
        title: 'Page Name',
        width: 60,
        className: 'text-left',
        render: (dataItem) => {
            const landingPageName = htmlDecode(dataItem.PageName);
            const landingPageCode = htmlDecode(dataItem.PageCode);
            return (
                <IframeHoverPreview src={`${props.landingPageUrl}/${dataItem.TargetLanguageCode}/${landingPageCode}`}>
                    <a href={`${props.landingPageUrl}/${dataItem.TargetLanguageCode}/${landingPageCode}`} target="_blank">
                        {landingPageName}
                    </a>
                </IframeHoverPreview>
            );
        },
        getTitle: (dataItem) => htmlDecode(dataItem.PageName)
    },
    {
        key: 'pageCode',
        title: 'Page Code',
        width: 50,
        className: 'text-left',
        render: (dataItem) => htmlDecode(dataItem.PageCode),
        getTitle: (dataItem) => htmlDecode(dataItem.PageCode)
    },
    {
        key: 'targetCountry',
        title: 'Target Country',
        width: 30,
        className: 'text-center',
        render: (dataItem) => htmlDecode(dataItem.TargetCountryName),
        getTitle: (dataItem) => htmlDecode(dataItem.TargetCountryName)
    },
    {
        key: 'language',
        title: 'Language',
        width: 40,
        className: 'text-center',
        render: (dataItem) => dataItem.Language || ''
    },
    {
        key: 'deal',
        title: 'Deal',
        width: 20,
        className: 'text-center text-primary',
        render: (dataItem) => dataItem.NoOfDeals || '',
        getTitle: (dataItem) => dataItem.Deals?.replace(/, /g, '\n') || ''
    },
    {
        key: 'createdDate',
        title: 'Created Date',
        width: 30,
        className: 'text-center',
        render: (dataItem) => formatDateTime(dataItem.CreatedDate, 'yyyy-MM-dd')
    },
    {
        key: 'createdBy',
        title: 'Created By',
        width: 30,
        className: 'text-center',
        render: (dataItem) => dataItem.CreatedBy || ''
    },
    {
        key: 'lastModifiedDate',
        title: 'Last Modified Date',
        width: 30,
        className: 'text-center',
        render: (dataItem) => formatDateTime(dataItem.LastModifiedDate, 'yyyy-MM-dd') || '--'
    },
    {
        key: 'lastModifiedBy',
        title: 'Last Modified By',
        width: 30,
        className: 'text-center',
        render: (dataItem) => dataItem.LastModifiedBy || '--'
    },
    {
        key: 'status',
        title: 'Status',
        width: 40,
        className: 'text-center',
        render: (dataItem) => {
            // Convert statusList to StatusDropdown format
            const getStatusOptions = () => {
                if (!props.statusList) return [];
                
                return props.statusList.map(status => {
                    const statusName = status.name.toLowerCase();
                    return {
                        value: status.key.toString(),
                        label: status.name,
                        type: statusName,
                        className: `status-select--${statusName}`
                    };
                });
            };

            return (
                <StatusDropdown
                    value={dataItem.StatusId?.toString() || ''}
                    onChange={(newStatusId) => props.onStatusChange && props.onStatusChange(dataItem.PageId, newStatusId)}
                    disabled={!props.isModifyInPageManagement}
                    statusOptions={getStatusOptions()}
                />
            );
        },
        getTitle: (dataItem) => {
            const statusItem = props.statusList && props.statusList.find(item => item.key === dataItem.StatusId);
            return statusItem?.name || '';
        }
    },
    {
        key: 'action',
        title: 'Action',
        width: 20,
        className: 'text-center',
        render: (dataItem) => (
            <>
                <a href={resolveClientUrl(`Portal/PageBuilder?pageId=${dataItem.PageId}`)} target="_top" title="Edit Landing Page">
                    <span className="oi oi-pencil text-secondary"></span>
                </a>
                <a className='ml-2' href={resolveClientUrl(`Portal/PageBuilder?pageId=${dataItem.PageId}&isClone=true`)} target="_top" title="Duplicate Landing Page">
                    <span className="oi oi-layers text-secondary"></span>
                </a>
            </>
        )
    }
];

/**
 * Asset Uploader Content Configuration
 */
export const assetUploaderContentConfig = (props) => [
    {
        key: 'index',
        title: '#',
        width: 10,
        className: 'text-center',
        render: (dataItem, index) => index + 1
    },
    {
        key: 'mediaName',
        title: 'Media Name',
        width: 40,
        className: 'text-left',
        render: (dataItem) => dataItem.MediaName || '',
        getTitle: (dataItem) => dataItem.MediaName || ''
    },
    {
        key: 'module',
        title: 'Module',
        width: 30,
        className: 'text-center',
        render: (dataItem) => dataItem.ModuleName || ''
    },
    {
        key: 'targetCountry',
        title: 'Target Country',
        width: 30,
        className: 'text-center',
        render: (dataItem) => dataItem.TargetCountryName || ''
    },
    {
        key: 'language',
        title: 'Language',
        width: 30,
        className: 'text-center',
        render: (dataItem) => dataItem.LanguageName || ''
    },
    {
        key: 'createdDate',
        title: 'Created Date',
        width: 30,
        className: 'text-center',
        render: (dataItem) => formatDateTime(dataItem.CreatedDate, 'yyyy-MM-dd')
    },
    {
        key: 'status',
        title: 'Status',
        width: 30,
        className: 'text-center',
        render: (dataItem) => {
            const statusClass = dataItem.StatusId === 1 ? 'status-active' : 'status-inactive';
            const statusText = dataItem.StatusId === 1 ? 'Active' : 'Inactive';
            return (
                <span className={`badge ${statusClass}`}>
                    {statusText}
                </span>
            );
        }
    },
    {
        key: 'action',
        title: 'Action',
        width: 20,
        className: 'text-center',
        render: (dataItem) => (
            <a href="#" onClick={(e) => {
                e.preventDefault();
                if (props.onPreview) {
                    props.onPreview(dataItem);
                }
            }} title="Preview">
                <span className="oi oi-eye text-secondary"></span>
            </a>
        )
    }
];

/**
 * Form Management Content Configuration
 */
export const formManagementContentConfig = (props) => [
    {
        key: 'index',
        title: '#',
        width: 10,
        className: 'text-center',
        render: (dataItem, index) => index + 1
    },
    {
        key: 'formName',
        title: 'Form Name',
        width: 40,
        className: 'text-left',
        render: (dataItem) => dataItem.FormName || '',
        getTitle: (dataItem) => dataItem.FormName || ''
    },
    {
        key: 'module',
        title: 'Module',
        width: 30,
        className: 'text-center',
        render: (dataItem) => dataItem.ModuleName || ''
    },
    {
        key: 'targetCountry',
        title: 'Target Country',
        width: 30,
        className: 'text-center',
        render: (dataItem) => dataItem.TargetCountryName || ''
    },
    {
        key: 'language',
        title: 'Language',
        width: 30,
        className: 'text-center',
        render: (dataItem) => dataItem.LanguageName || ''
    },
    {
        key: 'createdDate',
        title: 'Created Date',
        width: 30,
        className: 'text-center',
        render: (dataItem) => formatDateTime(dataItem.CreatedDate, 'yyyy-MM-dd')
    },
    {
        key: 'status',
        title: 'Status',
        width: 30,
        className: 'text-center',
        render: (dataItem) => {
            const statusClass = dataItem.StatusId === 1 ? 'status-active' : 'status-inactive';
            const statusText = dataItem.StatusId === 1 ? 'Active' : 'Inactive';
            return (
                <span className={`badge ${statusClass}`}>
                    {statusText}
                </span>
            );
        }
    },
    {
        key: 'action',
        title: 'Action',
        width: 20,
        className: 'text-center',
        render: (dataItem) => (
            <>
                <a href="#" onClick={(e) => {
                    e.preventDefault();
                    if (props.onEdit) {
                        props.onEdit(dataItem);
                    }
                }} title="Edit">
                    <span className="oi oi-pencil text-secondary"></span>
                </a>
                <a href="#" className="ml-2" onClick={(e) => {
                    e.preventDefault();
                    if (props.onPreview) {
                        props.onPreview(dataItem);
                    }
                }} title="Preview">
                    <span className="oi oi-eye text-secondary"></span>
                </a>
            </>
        )
    }
];
