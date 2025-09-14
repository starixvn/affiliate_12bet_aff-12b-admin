import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Input } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { resolveClientUrl, formatDateTime } from '../../../../modules/utils';
import { headerText, globalResource, messageSwitchStatus } from '../libs/constants';
import { setSelectedLandingPageId, setShouldResetScrollPosition, fetchData, setPagingOption, setNotification, toggleLandingPageStatus, setParam } from '../actions';

export const Content = (props) => {
    const { originalLanguageList, statusList } = props;

    useEffect(() => {
        const { param } = props;

        param && props.fetchData(param);
    }, [props.param]);
    
    const _onStatusChange = (pageId, newStatusId) => {
        const newStatus = statusList.find(item => item.key == newStatusId);
        console.log(statusList, newStatusId, newStatus);

        props.setNotification({
            message: `<p>Only Active pages are available for link generation.</p>
                <p>Inactive pages are not available for link generation. Existing links with an Inactive page will redirect users to the main join page.</p>
                <span>Are you sure you want to change status of this landing page?</span>`,
            isConfirmPopup: true,
            onConfirm: async () => {
                const result = await props.toggleLandingPageStatus({ pageId, statusId: newStatus.key });
                if (result && result.IsSuccessful) {
                    props.fetchData(props.param);
                }

                props.setNotification({
                    message: result?.IsSuccessful ? messageSwitchStatus.SwitchStatusSuccessfully : result?.Message || messageSwitchStatus.FailedToSwitchStatus,
                    isConfirmPopup: false
                });
            }
        });
    }

    const _handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, props.param, newPaging);

        props.setPagingOption(newPaging);
        props.setParam(newParam);
    };

    function _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={10} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={50} title={headerText.CampaignCode}>{headerText.CampaignCode}</Cell>
                <Cell header={true} colWidth={60} title={headerText.CampaignName}>{headerText.CampaignName}</Cell>
                <Cell header={true} colWidth={60} title={headerText.PageName}>{headerText.PageName}</Cell>
                <Cell header={true} colWidth={40} title={headerText.PageCode}>{headerText.PageCode}</Cell>
                <Cell header={true} colWidth={40} title={headerText.TargetCountry}>{headerText.TargetCountry}</Cell>
                <Cell header={true} colWidth={40} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Deal}>{headerText.Deal}</Cell>
                <Cell header={true} colWidth={40} title={headerText.CreatedDate}>{headerText.CreatedDate}</Cell>
                <Cell header={true} colWidth={40} title={headerText.CreatedBy}>{headerText.CreatedBy}</Cell>
                <Cell header={true} colWidth={40} title={headerText.LastModifiedDate}>{headerText.LastModifiedDate}</Cell>
                <Cell header={true} colWidth={40} title={headerText.LastModifiedBy}>{headerText.LastModifiedBy}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={20} title={headerText.Action}>{headerText.Action}</Cell>
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row key="no-info">
                <Cell className="text-center" colSpan="11">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    const _getStatusClass = (statusName) => {
        return `status-${statusName.toLowerCase()}`;
    }

    const _getTargetLanguageCode = (languageCode, targetCountryCode) => {
        const selectedLanguage = originalLanguageList?.find(lang => 
            lang.LanguageCode === languageCode && 
            lang.TargetCountryCode === targetCountryCode
        );

        return selectedLanguage?.TargetLanguageCode || selectedLanguage?.ShortedLanguageCode;
    }

    function _buildDataRow(dataItem, index) {
        const campaignCode = htmlDecode(dataItem.CampaignCode);
        const campaignName = htmlDecode(dataItem.CampaignName);
        const landingPageName = htmlDecode(dataItem.PageName);
        const landingPageCode = htmlDecode(dataItem.PageCode);
        const targetCountry = htmlDecode(dataItem.TargetCountryName);
        const language = dataItem.Language || '';
        const deal = dataItem.NoOfDeals || '';
        const dealList = dataItem.Deals?.replace(/, /g, '\n') || '';
        const createdDate = formatDateTime(dataItem.CreatedDate, 'yyyy-MM-dd');
        const lastModified = formatDateTime(dataItem.LastModifiedDate, 'yyyy-MM-dd') || '--';
        const statusItem = props.statusList && props.statusList.find(item => item.key === dataItem.StatusId);
        const statusClass = _getStatusClass(statusItem.name);
        const createdBy = dataItem.CreatedBy || '';
        const lastModifiedBy = dataItem.LastModifiedBy || '--';

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-left" title={campaignCode}>{campaignCode}</Cell>
                <Cell className="text-left" title={campaignName}>{campaignName}</Cell>
                <Cell className="text-left" title={landingPageName}>
                    <a href={`${props.landingPageUrl}/${_getTargetLanguageCode(dataItem.LanguageCode, dataItem.TargetCountryCode)}/${landingPageCode}`} target="_blank">{landingPageName}</a>
                </Cell>
                <Cell className="text-left" title={landingPageCode}>{landingPageCode}</Cell>
                <Cell className="text-left" title={targetCountry}>{targetCountry}</Cell>
                <Cell className="text-left" title={language}>{language}</Cell>
                <Cell className="text-center text-primary" title={dealList}>{deal}</Cell>
                <Cell className="text-left" title={createdDate}>{createdDate}</Cell>
                <Cell className="text-left" title={createdBy}>{createdBy}</Cell>
                <Cell className="text-left" title={lastModified}>{lastModified}</Cell>
                <Cell className="text-left" title={lastModifiedBy}>{lastModifiedBy}</Cell>
                <Cell className="text-left" title={statusItem.name}>
                    <Input 
                        type="select" 
                        className={`status-dropdown ${statusClass}`}
                        value={dataItem.StatusId || ''} 
                        onChange={(e) => _onStatusChange(dataItem.PageId, e.target.value)}
                    >
                        {props.statusList && props.statusList.map(item => (
                             <option 
                                 key={`status-${item.key}`} 
                                 value={item.key}
                                 className={_getStatusClass(item.name)}
                                 data-status={item.name.toLowerCase()}
                             >
                                 {item.name}
                             </option>
                         ))}
                    </Input>
                </Cell>
                <Cell className="text-center">
                    <a href={resolveClientUrl(`Portal/PageBuilder?pageId=${dataItem.PageId}`)} target="_top" title="Edit Landing Page">
                        <span className="oi oi-pencil text-secondary"></span>
                    </a>
                    <a className='ml-2' href={resolveClientUrl(`Portal/PageBuilder?pageId=${dataItem.PageId}&isClone=true`)} target="_top" title="Duplicate Landing Page">
                        <span className="oi oi-layers text-secondary"></span>
                    </a>
                </Cell>
            </Row>
        )
    }

    return props.gridData && (
        <div className="data-content">
            <div className="landing-page-content table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(props.gridData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={props.pagingOption}
                    onPaging={_handlePaging}
                    shouldResetScrollPosition={props.shouldResetScrollPosition}
                />
            </div>
        </div>
    );
}

function mapStateToProps({ shouldResetScrollPosition, gridData, pagingOption, param, statusList, landingPageUrl, originalLanguageList }) {
    return Object.assign({}, { shouldResetScrollPosition, gridData, pagingOption, param, statusList, landingPageUrl, originalLanguageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedLandingPageId, setShouldResetScrollPosition, fetchData, setPagingOption, setNotification, toggleLandingPageStatus, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));