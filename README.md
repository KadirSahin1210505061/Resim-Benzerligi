# Resim-Benzerligi

İlk olarak, gerekli using ifadelerini ekledik. Bu ifadeler, programda kullanacağımız sınıfları ve kütüphaneleri bildirmek için kullanılır.

Main metodunda programın başlangıcını belirtiyoruz.

imageFiles adlı bir string dizisi oluşturarak, belirtilen dizindeki görüntülerin dosya yolunu alıyoruz. Bu dizin, Directory.GetFiles metoduyla alınır.

images adlı bir List<Image<Bgr, byte>> oluşturarak görüntülerimizi depolayacağımız bir liste oluşturuyoruz.

processedImages adlı bir Dictionary<int, Image<Gray, float>> oluşturuyoruz. Bu sözlük, önceden işlenmiş görüntüleri depolamak için kullanılır. Burada anahtar olarak görüntü indeksini (int) ve değer olarak ise işlenmiş görüntüyü (Image<Gray, float>) kullanıyoruz.

Bir döngü aracılığıyla her bir görüntüyü images listesine ekliyoruz.

Görüntüyü gri tonlamaya çeviriyoruz (grayImage), ardından MatchTemplate yöntemini kullanarak görüntüyü önceden işlenmiş bir şekilde alıyoruz (processedImage). Bu işlem, görüntünün benzerlik analizi için gerekli olan ön işleme adımlarını gerçekleştirir.

Her bir görüntünün önceden işlenmiş hali olan processedImage'ı processedImages sözlüğüne ekliyoruz, burada anahtar olarak görüntü indeksini ve değer olarak ise işlenmiş görüntüyü kullanıyoruz.

FindMostSimilarImages metodunu çağırarak, en benzer görüntüleri bulmak için images ve processedImages değerlerini geçiyoruz. Bu metod, benzerlik analizini gerçekleştirir ve sonuçları bir liste olarak döndürür.

Benzerlik listesini ekrana yazdırıyoruz. Her bir benzerlik için, ilk görüntünün indeksi (Item1), ikinci görüntünün indeksi (Item2) ve benzerlik oranı (Item3) görüntülenir.

Son olarak, kullanıcının konsoldan bir tuşa basmasını beklemek için Console.ReadLine kullanıyoruz.

FindMostSimilarImages metodu:

İki tane iç içe döngü aracılığıyla her bir görüntü çifti için benzerlik hesaplaması yapılır. İç içe döngünün nedeni, aynı çiftin iki kez kontrol edilmemesidir.

CalculateSimilarity metodunu kullanarak iki işlenmiş görüntü arasındaki benzerlik oranını hesaplarız.

Benzerlik oranını bir Tuple<string, string, double> olarak oluşturarak similarityList listesine ekleriz. Bu tuple, birinci görüntünün indeksi (i.ToString()), ikinci görüntünün indeksi (j.ToString()) ve benzerlik oranı (similarity) bilgilerini içerir.

similarityList listesini benzerlik oranına göre sıralarız, Sort metodunu kullanarak. Bu sayede en yüksek benzerlik oranına sahip görüntü çiftleri listenin başına yerleşir.

Sıralanmış similarityList listesini döndürürüz.

CalculateSimilarity metodu:

processedImage1 ve processedImage2 arasında benzerlik oranını hesaplamak için MinMax metodunu kullanırız. Bu metod, her bir görüntünün piksel değerlerindeki minimum ve maksimum değerleri bulmamızı sağlar.

Benzerlik oranını, maksimum değerlerin karşılaştırılmasıyla hesaplarız. İki işlenmiş görüntünün maksimum değerlerinden hangisi daha büyükse, bu değeri benzerlik oranı olarak kabul ederiz ve geri döndürürüz.
