import axios from 'axios';
import React, { useState } from 'react';
function MeterUploader() {
    const [file, setfile] = useState(null);
    const [responseState, setResponseState] = useState(null);
    const [error, setError] = useState(null);

    const handleFileChange = (event) => {
        setfile(event.target.files[0]);
    }

    const handleUpload = async (event) => {
        event.preventDefault();

        if (!file) {
            return;
        }

        const formData = new FormData();
        formData.append('file', file);

        try {
            const response = await axios.post('https://localhost:7171/api/meter-reading-uploads', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

            setResponseState(response)
            setError(null)

        } catch (error) {
            setError(error)
            setResponseState(null)
        }
    };

    return (
        <div style={{ maxWidth: '500px', margin: '0 auto', textAlign: 'center' }} className="csv-upload-container">
            <h2>Upload Meter Readings</h2>
            <form onSubmit={handleUpload}>
                <input type="file" accept=".csv" onChange={handleFileChange} />
                <button type="submit">Upload</button>
            </form>
            {
                responseState != null &&
                <div>
                        <div className="respnses">
                            {responseState.data.reports.map((responseItem, key) => {
                                return (<div id={key}> {responseItem.reportMessage} </div>)
                            })}
                        </div>
                        <div className="valid-entries">Successful: {responseState.data.succesfulEntries}</div>
                        <div className="failed-entries">Failed: {responseState.data.failedEntries}</div>
                </div>
               
            }
            {

                error != null && error.response != null && error.response.data != null &&
                <div>
                        <div className="failed-entries">{error.message}</div>
                        <span> {error.response.data} </span>
                </div>
            }
        </div>
    );
}

export default MeterUploader;
