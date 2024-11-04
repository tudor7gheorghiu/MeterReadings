import React, { useState } from 'react';
import axios from 'axios';

function AccountsUploader() {
    const [file, setfile] = useState(null);

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
            const response = await axios.post('https://localhost:7171/api/accounts', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });

        } catch (error) {
        }
    };

    return (
        <div style={{ maxWidth: '500px', textAlign: 'center' }} className="csv-upload-container">
            <h2>Upload Accounts</h2>
            <form onSubmit={handleUpload}>
                <input type="file" accept=".csv" onChange={handleFileChange} />
                <button type="submit">Upload</button>
            </form>
        </div>
    );
}

export default AccountsUploader;
