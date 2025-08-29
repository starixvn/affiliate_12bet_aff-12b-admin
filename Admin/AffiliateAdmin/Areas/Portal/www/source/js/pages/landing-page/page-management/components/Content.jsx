import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button, Input } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { setSelectedLandingPageId, setShouldResetScrollPosition, fetchData, setPagingOption } from '../actions';

export const Content = (props) => {
    useEffect(() => {
        const { param } = props;

        param && props.fetchData(param);
    }, [props.param]);
    
    const _onDuplicateLandingPage = (landingPageId) => {
        // TODO: Implement duplicate functionality
        console.log('Duplicate landing page:', landingPageId);
    }

    const _onStatusChange = (landingPageId, newStatusId) => {
        // TODO: Implement status update functionality
        console.log('Update status for landing page:', landingPageId, 'to status:', newStatusId);
        // Here you would typically call an API to update the status
        // props.updateLandingPageStatus(landingPageId, newStatusId);
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
                <Cell header={true} colWidth={60} title={headerText.CampaignName}>{headerText.CampaignName}</Cell>
                <Cell header={true} colWidth={60} title={headerText.LandingPageName}>{headerText.LandingPageName}</Cell>
                <Cell header={true} colWidth={60} title={headerText.LandingPageCode}>{headerText.LandingPageCode}</Cell>
                <Cell header={true} colWidth={40} title={headerText.TargetCountry}>{headerText.TargetCountry}</Cell>
                <Cell header={true} colWidth={40} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={60} title={headerText.Deal}>{headerText.Deal}</Cell>
                <Cell header={true} colWidth={40} title={headerText.CreatedDate}>{headerText.CreatedDate}</Cell>
                <Cell header={true} colWidth={40} title={headerText.LastModified}>{headerText.LastModified}</Cell>
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

    function _buildDataRow(dataItem, index) {
        const campaignName = htmlDecode(dataItem.CampaignName);
        const landingPageName = htmlDecode(dataItem.PageName);
        const landingPageCode = htmlDecode(dataItem.PageCode);
        const targetCountry = htmlDecode(dataItem.TargetCountry);
        const language = dataItem.Language || 'English';
        const deal = dataItem.Deal || '';
        const createdDate = dataItem.CreatedDate || '';
        const lastModified = dataItem.LastModified || '';
        const statusItem = props.statusList && props.statusList.find(item => item.StatusId === dataItem.StatusId);
        const statusClass = _getStatusClass(statusItem.Status);

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-left" title={campaignName}>{campaignName}</Cell>
                <Cell className="text-left" title={landingPageName}>
                    <a href={`${props.landingPageUrl}/${landingPageCode}`} target="_blank">{landingPageName}</a>
                </Cell>
                <Cell className="text-left" title={landingPageCode}>{landingPageCode}</Cell>
                <Cell className="text-left" title={targetCountry}>{targetCountry}</Cell>
                <Cell className="text-left" title={language}>{language}</Cell>
                <Cell className="text-left" title={deal}>{deal}</Cell>
                <Cell className="text-left" title={createdDate}>{createdDate}</Cell>
                <Cell className="text-left" title={lastModified}>{lastModified}</Cell>
                <Cell className="text-left" title={statusItem.Status}>
                    <Input 
                        type="select" 
                        className={`status-dropdown ${statusClass}`}
                        value={dataItem.StatusId || ''} 
                        onChange={(e) => _onStatusChange(dataItem.PageId, e.target.value)}
                    >
                        {props.statusList && props.statusList.map(item => (
                             <option 
                                 key={`status-${item.StatusId}`} 
                                 value={item.StatusId}
                                 className={_getStatusClass(item.Status)}
                                 data-status={item.Status.toLowerCase()}
                             >
                                 {item.Status}
                             </option>
                         ))}
                    </Input>
                </Cell>
                <Cell className="text-center">
                    <Button color="link" onClick={() => props.setSelectedLandingPageId(dataItem.PageId)} title="Edit Landing Page">
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                    <Button color="link" onClick={() => _onDuplicateLandingPage(dataItem.PageId)} title="Duplicate Landing Page">
                        <span className="oi oi-layers text-secondary"></span>
                    </Button>
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

function mapStateToProps({ shouldResetScrollPosition, gridData, pagingOption, param, statusList, landingPageUrl }) {
    return Object.assign({}, { shouldResetScrollPosition, gridData, pagingOption, param, statusList, landingPageUrl });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedLandingPageId, setShouldResetScrollPosition, fetchData, setPagingOption }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));