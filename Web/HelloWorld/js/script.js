var count = 0;

document.getElementById('clickMeButton').addEventListener('click', function() 
{
    // Update count
    count = count + 1;

    // Create message
    var buttonLabel = document.getElementById('clickMeButton').innerText;
    var message = `You clicked the '${buttonLabel}' button ${count} times.`;

    // Show message offcreen (for debugging)
    console.log(message);

    // Show message onscreen (for end user)
    document.getElementById('statusText').textContent = message;
});






