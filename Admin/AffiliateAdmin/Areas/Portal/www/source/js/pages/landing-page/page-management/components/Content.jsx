import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { setSelectedLandingPageId, setShouldResetScrollPosition, fetchData } from '../actions';

export const Content = (props) => {
    useEffect(() => {
        const { param } = props;

        param && props.fetchData(param);
    }, [props.param]);
    
    const _onDuplicateLandingPage = (landingPageId) => {
        // TODO: Implement duplicate functionality
        console.log('Duplicate landing page:', landingPageId);
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
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={100} title={headerText.LandingPageName}>{headerText.LandingPageName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.LandingPageCode}>{headerText.LandingPageCode}</Cell>
                <Cell header={true} colWidth={50} title={headerText.TargetCountry}>{headerText.TargetCountry}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Deal}>{headerText.Deal}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Action}>{headerText.Action}</Cell>
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row key="no-info">
                <Cell className="text-center" colSpan="8">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    function _buildDataRow(dataItem, index) {
        const landingPageName = htmlDecode(dataItem.PageName);
        const landingPageCode = htmlDecode(dataItem.PageCode);
        const blockTargetCountry = htmlDecode(dataItem.BlockTargetCountry).split(',').join(', ');
        const language = dataItem.Language || 'English';
        const deal = dataItem.Deal || '';
        const status = dataItem.Status || '';

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-left" title={landingPageName}>{landingPageName}</Cell>
                <Cell className="text-left" title={landingPageCode}>{landingPageCode}</Cell>
                <Cell className="text-left" title={blockTargetCountry}>{blockTargetCountry}</Cell>
                <Cell className="text-center" title={language}>{language}</Cell>
                <Cell className="text-center" title={deal}>{deal}</Cell>
                <Cell className="text-center" title={status}>{status}</Cell>
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

    return (
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

function mapStateToProps({ shouldResetScrollPosition, gridData, pagingOption, param }) {
    return Object.assign({}, { shouldResetScrollPosition, gridData, pagingOption, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedLandingPageId, setShouldResetScrollPosition, fetchData, setPagingOption }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));