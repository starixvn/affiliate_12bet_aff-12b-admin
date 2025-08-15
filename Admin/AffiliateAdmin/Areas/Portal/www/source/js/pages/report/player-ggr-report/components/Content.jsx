import React, { Component } from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import GridData from './GridData';
import ReportTitle from '../../../../components/ReportTitle';

import { toggleLoader } from '../../../../actions/loaderAction';
import ReportExcel from '../libs/reportExcel';
import { fetchData } from '../actions';

export class Content extends Component {
    constructor(props) {
        super(props);
        this.state = {
            exportProgress: null,
            exportStatus: null,
            isExporting: false
        };
    }

    shouldComponentUpdate(nextProps, nextState) {
        const { reportTitle } = this.props;
        const { exportProgress, exportStatus, isExporting } = this.state;

        return nextProps.reportTitle !== reportTitle || 
               nextState.exportProgress !== exportProgress ||
               nextState.exportStatus !== exportStatus ||
               nextState.isExporting !== isExporting;
    }

    handleExportExcel = async () => {
        // Prevent multiple simultaneous exports
        if (this.state.isExporting) {
            alert('Export is already in progress. Please wait...');
            return;
        }

        this.setState({ isExporting: true });
        this.props.toggleLoader(true);
        const { param, reportTitle, productList } = this.props;
        const selectedProducts = productList.filter(p => p.checked);
        
        try {
            // First, get total count to determine if we need chunked processing
            const countParam = Object.assign({}, param, { IsExportExcel: true, PageSize: 1 });
            const countData = await this.props.fetchData(countParam);
            
            if (!countData || !countData.TotalItem) {
                alert('No data to export');
                return;
            }

            const totalRecords = countData.TotalItem;
            const maxChunkSize = 1000; // Process 1000 records at a time
            const totalProducts = selectedProducts.length;
            
            // For large datasets (>5000 records OR >100 products), use chunked processing
            if (totalRecords > 5000 || totalProducts > 100) {
                await this.handleLargeDatasetExport(param, reportTitle, selectedProducts, totalRecords, maxChunkSize);
            } else {
                // Use existing approach for smaller datasets
                await this.handleStandardExport(param, reportTitle, selectedProducts);
            }
        } catch (error) {
            console.error('Export failed:', error);
            alert('Export failed. Please try again or contact support.');
        } finally {
            this.setState({ 
                isExporting: false,
                exportProgress: null,
                exportStatus: null 
            });
            this.props.toggleLoader(false);
        }
    }

    handleStandardExport = async (param, reportTitle, selectedProducts) => {
        const newParam = Object.assign({}, param, { IsExportExcel: true });
        const data = await this.props.fetchData(newParam);

        if (data) {
            const reportExcel = new ReportExcel(data, reportTitle, selectedProducts);
            reportExcel.export();
        }
    }

    handleLargeDatasetExport = async (param, reportTitle, selectedProducts, totalRecords, chunkSize) => {
        const chunks = Math.ceil(totalRecords / chunkSize);
        let allData = [];
        let totalData = null;
        
        // Initialize progress tracking
        this.setState({ 
            exportProgress: 0, 
            exportStatus: `Preparing to export ${totalRecords.toLocaleString()} records...` 
        });
        
        // Process data in chunks
        for (let i = 0; i < chunks; i++) {
            const chunkParam = Object.assign({}, param, {
                IsExportExcel: true,
                PageIndex: i + 1,
                PageSize: chunkSize
            });
            
            // Update status
            this.setState({ 
                exportStatus: `Fetching data chunk ${i + 1} of ${chunks}...` 
            });
            
            const chunkData = await this.props.fetchData(chunkParam);
            
            if (chunkData && chunkData.MainData) {
                allData = allData.concat(chunkData.MainData);
                
                // Store total data from first chunk
                if (i === 0) {
                    totalData = chunkData.Total;
                }
                
                // Update progress
                const progress = Math.round(((i + 1) / chunks) * 100);
                this.setState({ 
                    exportProgress: progress,
                    exportStatus: `Downloaded ${allData.length.toLocaleString()} of ${totalRecords.toLocaleString()} records...`
                });
            }
            
            // Small delay to prevent overwhelming the server
            if (i < chunks - 1) {
                await new Promise(resolve => setTimeout(resolve, 100));
            }
        }
        
        // Update status for Excel generation
        this.setState({ 
            exportStatus: 'Generating Excel file...' 
        });
        
        // Create Excel with combined data
        const combinedData = {
            MainData: allData,
            Total: totalData,
            TotalItem: totalRecords
        };
        
        const reportExcel = new ReportExcel(combinedData, reportTitle, selectedProducts);
        reportExcel.export();
        
        // Clear progress
        this.setState({ 
            exportProgress: null, 
            exportStatus: null 
        });
    }

    _buildReportTitle() {
        const { reportTitle } = this.props;
        const { isExporting } = this.state;

        return (
            <div className="d-flex align-items-center">
                {reportTitle}
                <button 
                    type="button" 
                    className={`ml-auto btn btn-link btn-excel ${isExporting ? 'disabled' : ''}`}
                    onClick={this.handleExportExcel} 
                    disabled={isExporting}
                    title={isExporting ? "Export in progress..." : "Export Excel"}>
                    <span className={`oi ${isExporting ? 'oi-clock' : 'icon-excel'}`}></span>
                    {isExporting && <span className="ml-1">Exporting...</span>}
                </button>
            </div>
        )
    }

    render() {
        const { reportTitle } = this.props;
        const { exportProgress, exportStatus } = this.state;

        return (
            <div className="data-content my-2 report-content-container">
                {
                    reportTitle && (
                        <ReportTitle content={this._buildReportTitle()} />
                    )
                }
                {
                    exportProgress !== null && (
                        <div className="export-progress-container mb-3 p-3 bg-light border rounded">
                            <div className="d-flex justify-content-between align-items-center mb-2">
                                <span className="font-weight-bold">Excel Export Progress</span>
                                <span className="badge badge-info">{exportProgress}%</span>
                            </div>
                            <div className="progress mb-2">
                                <div 
                                    className="progress-bar progress-bar-striped progress-bar-animated" 
                                    role="progressbar" 
                                    style={{ width: `${exportProgress}%` }}
                                    aria-valuenow={exportProgress} 
                                    aria-valuemin="0" 
                                    aria-valuemax="100">
                                </div>
                            </div>
                            <small className="text-muted">{exportStatus}</small>
                        </div>
                    )
                }
                <GridData />
            </div>
        );
    }
}

function mapStateToProps({ reportTitle, param, productList }) {
    return Object.assign({}, { reportTitle, param, productList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);